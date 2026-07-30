using BrainDump.Application.Common.Interfaces;
using BrainDump.Application.DTOs.AI;
using BrainDump.Domain.Entities;

namespace BrainDump.Infrastructure.AI;

/// <summary>
/// Implementação Mock do serviço de classificação/parsing de tarefas por IA em português.
/// </summary>
public class MockItemClassifierService : IItemClassifierService
{
    public Task<IEnumerable<ParsedTaskDraft>> ClassifyAndExtractItemsAsync(
        string transcribedText,
        DateTime referenceDate,
        CancellationToken cancellationToken = default)
    {
        var drafts = new List<ParsedTaskDraft>();

        if (string.IsNullOrWhiteSpace(transcribedText))
        {
            return Task.FromResult<IEnumerable<ParsedTaskDraft>>(drafts);
        }

        var lower = transcribedText.ToLowerInvariant();

        if (lower.Contains("comprar") || lower.Contains("leite") || lower.Contains("mercado"))
        {
            var tomorrow = referenceDate.Date.AddDays(1).AddHours(15);
            drafts.Add(new ParsedTaskDraft(
                Title: "Comprar leite no mercado",
                Category: Category.Shopping,
                Priority: Priority.Medium,
                DueDate: tomorrow,
                ConfidenceScore: 0.92f));
        }

        if (lower.Contains("médica") || lower.Contains("consulta") || lower.Contains("médico"))
        {
            // Calcula próxima sexta-feira
            var daysUntilFriday = ((int)DayOfWeek.Friday - (int)referenceDate.DayOfWeek + 7) % 7;
            if (daysUntilFriday == 0) daysUntilFriday = 7;
            var fridayDate = referenceDate.Date.AddDays(daysUntilFriday).AddHours(17);

            drafts.Add(new ParsedTaskDraft(
                Title: "Marcar consulta médica",
                Category: Category.Health,
                Priority: Priority.High,
                DueDate: fridayDate,
                ConfidenceScore: 0.90f));
        }

        // Se não identificou nenhum padrão específico, adiciona o próprio texto limpo
        if (!drafts.Any())
        {
            drafts.Add(new ParsedTaskDraft(
                Title: transcribedText.Trim(),
                Category: Category.Personal,
                Priority: Priority.Medium,
                DueDate: null,
                ConfidenceScore: 0.80f));
        }

        return Task.FromResult<IEnumerable<ParsedTaskDraft>>(drafts);
    }
}
