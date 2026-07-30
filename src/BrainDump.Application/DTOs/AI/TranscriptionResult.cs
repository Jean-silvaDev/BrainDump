namespace BrainDump.Application.DTOs.AI;

/// <summary>
/// Resultado da transcrição de áudio via STT (Speech-to-Text).
/// </summary>
public record TranscriptionResult(
    string Text,
    float ConfidenceScore,
    string DetectedLanguage);
