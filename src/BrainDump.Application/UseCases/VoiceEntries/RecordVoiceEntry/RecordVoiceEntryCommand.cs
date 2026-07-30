namespace BrainDump.Application.UseCases.VoiceEntries.RecordVoiceEntry;

/// <summary>
/// Comando contendo os dados para gravar e cadastrar uma entrada de voz.
/// </summary>
public record RecordVoiceEntryCommand(
    Guid UserId,
    Stream AudioStream,
    string FileName,
    string ContentType,
    int DurationSeconds,
    long ByteSize);
