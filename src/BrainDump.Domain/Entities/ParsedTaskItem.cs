namespace BrainDump.Domain.Entities;

/// <summary>
/// Entidade de domínio que representa um item de tarefa extraído do áudio por IA.
/// </summary>
public class ParsedTaskItem
{
    public Guid Id { get; private set; }
    public Guid VoiceEntryId { get; private set; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; }
    public Category Category { get; private set; }
    public Priority Priority { get; private set; }
    public DateTime? DueDate { get; private set; }
    public float ConfidenceScore { get; private set; }
    public ParsedTaskStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private ParsedTaskItem()
    {
        Title = string.Empty;
    }

    public ParsedTaskItem(
        Guid id,
        Guid voiceEntryId,
        Guid userId,
        string title,
        Category category,
        Priority priority,
        DateTime? dueDate,
        float confidenceScore,
        DateTime createdAt)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("ID do item não pode ser vazio.", nameof(id));

        if (voiceEntryId == Guid.Empty)
            throw new ArgumentException("ID da captura de voz não pode ser vazio.", nameof(voiceEntryId));

        if (userId == Guid.Empty)
            throw new ArgumentException("ID do usuário não pode ser vazio.", nameof(userId));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("O título da tarefa é obrigatório.", nameof(title));

        if (confidenceScore < 0.0f || confidenceScore > 1.0f)
            throw new ArgumentOutOfRangeException(nameof(confidenceScore), "A nota de confiança deve estar entre 0.0 e 1.0.");

        Id = id;
        VoiceEntryId = voiceEntryId;
        UserId = userId;
        Title = title.Trim();
        Category = category;
        Priority = priority;
        DueDate = dueDate;
        ConfidenceScore = confidenceScore;
        Status = ParsedTaskStatus.PendingReview;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
    }

    public static ParsedTaskItem Create(
        Guid voiceEntryId,
        Guid userId,
        string title,
        Category category,
        Priority priority,
        DateTime? dueDate,
        float confidenceScore = 1.0f)
    {
        return new ParsedTaskItem(
            Guid.NewGuid(),
            voiceEntryId,
            userId,
            title,
            category,
            priority,
            dueDate,
            confidenceScore,
            DateTime.UtcNow);
    }

    public void UpdateDetails(string newTitle, Category newCategory, Priority newPriority, DateTime? newDueDate)
    {
        if (string.IsNullOrWhiteSpace(newTitle))
            throw new ArgumentException("O título da tarefa não pode ser vazio.", nameof(newTitle));

        Title = newTitle.Trim();
        Category = newCategory;
        Priority = newPriority;
        DueDate = newDueDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Approve()
    {
        Status = ParsedTaskStatus.Approved;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Discard()
    {
        Status = ParsedTaskStatus.Discarded;
        UpdatedAt = DateTime.UtcNow;
    }
}
