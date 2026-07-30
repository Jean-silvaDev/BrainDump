using BrainDump.Application.DTOs.AI;

namespace BrainDump.Application.Common.Interfaces;

/// <summary>
/// Porta para o serviço de inteligência artificial responsável por separar e classificar o texto transcrito em tarefas.
/// </summary>
public interface IItemClassifierService
{
    /// <summary>
    /// Extrai itens de tarefas com categoria, prioridade e prazo calculados.
    /// </summary>
    /// <param name="transcribedText">Texto transcrito do áudio.</param>
    /// <param name="referenceDate">Data/hora UTC de referência para cálculo de prazos relativos ("até sexta").</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Lista de rascunhos de tarefas extraídas.</returns>
    Task<IEnumerable<ParsedTaskDraft>> ClassifyAndExtractItemsAsync(
        string transcribedText,
        DateTime referenceDate,
        CancellationToken cancellationToken = default);
}
