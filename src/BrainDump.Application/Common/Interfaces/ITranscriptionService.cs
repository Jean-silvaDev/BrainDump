using BrainDump.Application.DTOs.AI;

namespace BrainDump.Application.Common.Interfaces;

/// <summary>
/// Porta da camada de aplicação para o serviço de transcrição de áudio em texto (STT).
/// </summary>
public interface ITranscriptionService
{
    /// <summary>
    /// Transcreve o stream de áudio fornecido.
    /// </summary>
    Task<TranscriptionResult> TranscribeAudioAsync(
        Stream audioStream,
        string audioFormat,
        CancellationToken cancellationToken = default);
}
