namespace BrainDump.Domain.Entities;

/// <summary>
/// Entidade de domínio que representa uma captura de voz realizada pelo usuário.
/// </summary>
public class VoiceEntry
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string AudioFilePath { get; private set; }
    public string AudioFormat { get; private set; }
    public int DurationSeconds { get; private set; }
    public long ByteSize { get; private set; }
    public VoiceEntryStatus Status { get; private set; }
    public string? TranscribedText { get; private set; }
    public string? FailureReason { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private VoiceEntry()
    {
        AudioFilePath = string.Empty;
        AudioFormat = string.Empty;
    }

    public VoiceEntry(
        Guid id,
        Guid userId,
        string audioFilePath,
        string audioFormat,
        int durationSeconds,
        long byteSize,
        DateTime createdAt)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("O ID da gravação de voz não pode ser vazio.", nameof(id));

        if (userId == Guid.Empty)
            throw new ArgumentException("O ID do usuário é obrigatório.", nameof(userId));

        if (string.IsNullOrWhiteSpace(audioFilePath))
            throw new ArgumentException("O caminho do arquivo de áudio é obrigatório.", nameof(audioFilePath));

        if (string.IsNullOrWhiteSpace(audioFormat))
            throw new ArgumentException("O formato do áudio é obrigatório.", nameof(audioFormat));

        if (durationSeconds < 0)
            throw new ArgumentException("A duração do áudio não pode ser negativa.", nameof(durationSeconds));

        if (byteSize <= 0)
            throw new ArgumentException("O tamanho do áudio deve ser maior que zero.", nameof(byteSize));

        Id = id;
        UserId = userId;
        AudioFilePath = audioFilePath;
        AudioFormat = audioFormat.ToLowerInvariant();
        DurationSeconds = durationSeconds;
        ByteSize = byteSize;
        Status = VoiceEntryStatus.PendingTranscription;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
    }

    public static VoiceEntry Create(
        Guid userId,
        string audioFilePath,
        string audioFormat,
        int durationSeconds,
        long byteSize)
    {
        return new VoiceEntry(
            Guid.NewGuid(),
            userId,
            audioFilePath,
            audioFormat,
            durationSeconds,
            byteSize,
            DateTime.UtcNow);
    }

    public void MarkAsProcessing()
    {
        if (Status == VoiceEntryStatus.Completed)
            throw new InvalidOperationException("Não é possível reprocessar uma captura de voz já concluída.");

        Status = VoiceEntryStatus.Processing;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsCompleted(string transcribedText)
    {
        if (string.IsNullOrWhiteSpace(transcribedText))
            throw new ArgumentException("O texto transcrito não pode ser vazio.", nameof(transcribedText));

        Status = VoiceEntryStatus.Completed;
        TranscribedText = transcribedText;
        FailureReason = null;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed(string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException(" O motivo da falha deve ser fornecido.", nameof(reason));

        Status = VoiceEntryStatus.Failed;
        FailureReason = reason;
        UpdatedAt = DateTime.UtcNow;
    }
}
