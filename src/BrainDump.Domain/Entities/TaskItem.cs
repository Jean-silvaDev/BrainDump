namespace BrainDump.Domain.Entities;

/// <summary>
/// Entidade de domínio que representa uma tarefa oficial confirmada pelo usuário.
/// </summary>
public class TaskItem
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; }
    public Category Category { get; private set; }
    public Priority Priority { get; private set; }
    public DateTime? DueDate { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private TaskItem()
    {
        Title = string.Empty;
    }

    public TaskItem(
        Guid id,
        Guid userId,
        string title,
        Category category,
        Priority priority,
        DateTime? dueDate,
        DateTime createdAt)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("O ID da tarefa não pode ser vazio.", nameof(id));

        if (userId == Guid.Empty)
            throw new ArgumentException("O ID do usuário não pode ser vazio.", nameof(userId));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("O título da tarefa não pode ser vazio.", nameof(title));

        Id = id;
        UserId = userId;
        Title = title.Trim();
        Category = category;
        Priority = priority;
        DueDate = dueDate;
        IsCompleted = false;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
    }

    public static TaskItem Create(
        Guid userId,
        string title,
        Category category,
        Priority priority,
        DateTime? dueDate)
    {
        return new TaskItem(
            Guid.NewGuid(),
            userId,
            title,
            category,
            priority,
            dueDate,
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

    public void MarkAsCompleted()
    {
        IsCompleted = true;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reopen()
    {
        IsCompleted = false;
        CompletedAt = null;
        UpdatedAt = DateTime.UtcNow;
    }
}
