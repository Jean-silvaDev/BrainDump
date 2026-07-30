using BrainDump.Application.Common.Interfaces;
using BrainDump.Application.DTOs.AI;

namespace BrainDump.Infrastructure.AI;

/// <summary>
/// Implementação Mock do serviço de transcrição (STT) para testes e desenvolvimento sem custo de API.
/// </summary>
public class MockTranscriptionService : ITranscriptionService
{
    public Task<TranscriptionResult> TranscribeAudioAsync(
        Stream audioStream,
        string audioFormat,
        CancellationToken cancellationToken = default)
    {
        // Retorna um texto transcrito simulado realista
        var result = new TranscriptionResult(
            Text: "Lembrar de comprar leite no mercado amanhã às 15 horas e marcar consulta médica até sexta-feira",
            ConfidenceScore: 0.95f,
            DetectedLanguage: "pt-BR");

        return Task.FromResult(result);
    }
}
