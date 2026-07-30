using BrainDump.Domain.Entities;

namespace BrainDump.Application.DTOs.Voice;

/// <summary>
/// DTO de resposta para operações com registros de voz.
/// </summary>
public record VoiceEntryResponse(
    Guid Id,
    Guid UserId,
    string AudioFilePath,
    string AudioFormat,
    int DurationSeconds,
    long ByteSize,
    VoiceEntryStatus Status,
    string? TranscribedText,
    DateTime CreatedAt);
