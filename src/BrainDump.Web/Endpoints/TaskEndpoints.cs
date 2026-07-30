using System.Security.Claims;
using BrainDump.Application.DTOs.Tasks;
using BrainDump.Application.UseCases.Tasks.DeleteTask;
using BrainDump.Application.UseCases.Tasks.EditTask;
using BrainDump.Application.UseCases.Tasks.GetTasks;
using BrainDump.Application.UseCases.Tasks.ToggleTaskCompletion;
using BrainDump.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BrainDump.Web.Endpoints;

public static class TaskEndpoints
{
    public static void MapTaskEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tasks").WithTags("Lista de Tarefas").RequireAuthorization();

        // 1. Listar tarefas do usuário com filtros
        group.MapGet("/", async (
            [FromQuery] Category? category,
            [FromQuery] Priority? priority,
            [FromQuery] bool? isCompleted,
            [FromQuery] string? search,
            ClaimsPrincipal user,
            GetTasksUseCase useCase,
            CancellationToken ct) =>
        {
            var userId = GetUserId(user);
            if (userId == Guid.Empty) return Results.Unauthorized();

            var query = new GetTasksQuery(category, priority, isCompleted, search);
            var tasks = await useCase.ExecuteAsync(userId, query, ct);
            return Results.Ok(tasks);
        })
        .WithName("GetTasks")
        .WithSummary("Retorna a lista de tarefas do usuário com suporte a filtros por categoria, prioridade e status");

        // 2. Alternar status de conclusão
        group.MapPatch("/{id:guid}/toggle", async (
            Guid id,
            ClaimsPrincipal user,
            ToggleTaskCompletionUseCase useCase,
            CancellationToken ct) =>
        {
            var userId = GetUserId(user);
            if (userId == Guid.Empty) return Results.Unauthorized();

            try
            {
                var updated = await useCase.ExecuteAsync(id, userId, ct);
                return Results.Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        })
        .WithName("ToggleTaskCompletion")
        .WithSummary("Alterna o status da tarefa entre concluída e pendente");

        // 3. Editar tarefa
        group.MapPut("/{id:guid}", async (
            Guid id,
            [FromBody] EditTaskRequest request,
            ClaimsPrincipal user,
            EditTaskUseCase useCase,
            CancellationToken ct) =>
        {
            var userId = GetUserId(user);
            if (userId == Guid.Empty) return Results.Unauthorized();

            try
            {
                var updated = await useCase.ExecuteAsync(id, userId, request, ct);
                return Results.Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        })
        .WithName("EditTask")
        .WithSummary("Edita o título, categoria, prioridade ou prazo de uma tarefa oficial");

        // 4. Excluir tarefa
        group.MapDelete("/{id:guid}", async (
            Guid id,
            ClaimsPrincipal user,
            DeleteTaskUseCase useCase,
            CancellationToken ct) =>
        {
            var userId = GetUserId(user);
            if (userId == Guid.Empty) return Results.Unauthorized();

            try
            {
                await useCase.ExecuteAsync(id, userId, ct);
                return Results.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        })
        .WithName("DeleteTask")
        .WithSummary("Exclui permanentemente uma tarefa");
    }

    private static Guid GetUserId(ClaimsPrincipal user)
    {
        var claim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                 ?? user.FindFirst("sub")?.Value;

        return Guid.TryParse(claim, out var userId) ? userId : Guid.Empty;
    }
}
