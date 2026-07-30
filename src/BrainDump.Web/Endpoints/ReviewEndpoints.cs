using System.Security.Claims;
using BrainDump.Application.DTOs.Review;
using BrainDump.Application.UseCases.Review.ConfirmParsedTasks;
using BrainDump.Application.UseCases.Review.DiscardParsedTaskItem;
using BrainDump.Application.UseCases.Review.GetPendingReviewItems;
using BrainDump.Application.UseCases.Review.UpdateParsedTaskItem;
using Microsoft.AspNetCore.Mvc;

namespace BrainDump.Web.Endpoints;

public static class ReviewEndpoints
{
    public static void MapReviewEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/review").WithTags("Tela de Revisão").RequireAuthorization();

        // 1. Listar itens pendentes de revisão
        group.MapGet("/items", async (
            ClaimsPrincipal user,
            GetPendingReviewItemsUseCase useCase,
            CancellationToken ct) =>
        {
            var userId = GetUserId(user);
            if (userId == Guid.Empty) return Results.Unauthorized();

            var items = await useCase.ExecuteAsync(userId, ct);
            return Results.Ok(items);
        })
        .WithName("GetPendingReviewItems")
        .WithSummary("Retorna todos os rascunhos de tarefas aguardando revisão do usuário");

        // 2. Atualizar/Editar um rascunho
        group.MapPut("/items/{id:guid}", async (
            Guid id,
            [FromBody] UpdateParsedTaskItemRequest request,
            ClaimsPrincipal user,
            UpdateParsedTaskItemUseCase useCase,
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
        .WithName("UpdateParsedTaskItem")
        .WithSummary("Edita os dados de um rascunho de tarefa");

        // 3. Confirmar e converter rascunhos em tarefas oficiais
        group.MapPost("/confirm", async (
            [FromBody] ConfirmTasksRequest request,
            ClaimsPrincipal user,
            ConfirmParsedTasksUseCase useCase,
            CancellationToken ct) =>
        {
            var userId = GetUserId(user);
            if (userId == Guid.Empty) return Results.Unauthorized();

            var createdTasks = await useCase.ExecuteAsync(userId, request, ct);
            return Results.Ok(createdTasks);
        })
        .WithName("ConfirmParsedTasks")
        .WithSummary("Confirma rascunhos e gera as tarefas oficiais confirmadas");

        // 4. Descartar um rascunho
        group.MapDelete("/items/{id:guid}", async (
            Guid id,
            ClaimsPrincipal user,
            DiscardParsedTaskItemUseCase useCase,
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
        .WithName("DiscardParsedTaskItem")
        .WithSummary("Descarta um rascunho de tarefa");
    }

    private static Guid GetUserId(ClaimsPrincipal user)
    {
        var claim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                 ?? user.FindFirst("sub")?.Value;

        return Guid.TryParse(claim, out var userId) ? userId : Guid.Empty;
    }
}
