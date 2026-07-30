namespace BrainDump.Domain.Entities;

/// <summary>
/// Status da tarefa extraída antes da confirmação final pelo usuário.
/// </summary>
public enum ParsedTaskStatus
{
    /// <summary>
    /// Aguardando revisão do usuário.
    /// </summary>
    PendingReview = 1,

    /// <summary>
    /// Aprovada/confirmada pelo usuário e convertida em tarefa final.
    /// </summary>
    Approved = 2,

    /// <summary>
    /// Descartada pelo usuário na revisão.
    /// </summary>
    Discarded = 3
}
