using System.Security.Claims;
using BrainDump.Application.UseCases.VoiceEntries.RecordVoiceEntry;
using Microsoft.AspNetCore.Mvc;

namespace BrainDump.Web.Endpoints;

public static class VoiceEndpoints
{
    public static void MapVoiceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/voice").WithTags("Captura de Voz").RequireAuthorization();

        group.MapPost("/entries", async (
            IFormFile file,
            [FromForm] int? durationSeconds,
            ClaimsPrincipal user,
            RecordVoiceEntryUseCase useCase,
            CancellationToken ct) =>
        {
            if (file == null || file.Length == 0)
            {
                return Results.BadRequest(new { error = "Nenhum arquivo de áudio foi enviado." });
            }

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                           ?? user.FindFirst("sub")?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Results.Unauthorized();
            }

            try
            {
                using var stream = file.OpenReadStream();
                var command = new RecordVoiceEntryCommand(
                    userId,
                    stream,
                    file.FileName,
                    file.ContentType,
                    durationSeconds ?? 0,
                    file.Length);

                var response = await useCase.ExecuteAsync(command, ct);

                return Results.Accepted($"/api/voice/entries/{response.Id}", response);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Results.UnprocessableEntity(new { error = ex.Message });
            }
        })
        .DisableAntiforgery()
        .WithName("UploadVoiceEntry")
        .WithSummary("Recebe um arquivo de áudio gravado e registra a entrada de voz para processamento assíncrono");
    }
}
