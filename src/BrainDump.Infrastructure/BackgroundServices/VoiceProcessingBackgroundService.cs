using BrainDump.Application.UseCases.VoiceEntries.ProcessVoiceEntryTranscription;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BrainDump.Infrastructure.BackgroundServices;

/// <summary>
/// Serviço de segundo plano (IHostedService) que consome a fila e dispara o caso de uso de transcrição e parsing.
/// </summary>
public class VoiceProcessingBackgroundService : BackgroundService
{
    private readonly VoiceProcessingQueue _queue;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<VoiceProcessingBackgroundService> _logger;

    public VoiceProcessingBackgroundService(
        VoiceProcessingQueue queue,
        IServiceProvider serviceProvider,
        ILogger<VoiceProcessingBackgroundService> logger)
    {
        _queue = queue;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Serviço de segundo plano para processamento de voz iniciado.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var voiceEntryId = await _queue.DequeueAsync(stoppingToken);

                _logger.LogInformation("Iniciando transcrição do áudio {VoiceEntryId}...", voiceEntryId);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var useCase = scope.ServiceProvider.GetRequiredService<ProcessVoiceEntryTranscriptionUseCase>();
                    await useCase.ExecuteAsync(voiceEntryId, stoppingToken);
                }

                _logger.LogInformation("Transcrição e parsing do áudio {VoiceEntryId} concluídos com sucesso.", voiceEntryId);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro no processamento em segundo plano do áudio.");
            }
        }
    }
}
