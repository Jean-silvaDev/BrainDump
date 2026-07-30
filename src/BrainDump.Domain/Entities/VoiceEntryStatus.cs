namespace BrainDump.Domain.Entities;

/// <summary>
/// Representa os possíveis estados do ciclo de vida de uma entrada de voz.
/// </summary>
public enum VoiceEntryStatus
{
    /// <summary>
    /// Áudio recebido e armazenado, aguardando transcrição.
    /// </summary>
    PendingTranscription = 1,

    /// <summary>
    /// Transcrição ou parsing por IA em andamento.
    /// </summary>
    Processing = 2,

    /// <summary>
    /// Transcrição e parsing concluídos com sucesso.
    /// </summary>
    Completed = 3,

    /// <summary>
    /// Falha durante a transcrição ou processamento do áudio.
    /// </summary>
    Failed = 4
}
