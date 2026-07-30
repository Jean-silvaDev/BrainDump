using BrainDump.Application.Common.Interfaces;
using BrainDump.Domain.Entities;
using BrainDump.Domain.Repositories;

namespace BrainDump.Application.UseCases.VoiceEntries.ProcessVoiceEntryTranscription;

/// <summary>
/// Caso de uso orquestrador do fluxo completo de transcrição e parsing por IA.
/// </summary>
public class ProcessVoiceEntryTranscriptionUseCase
{
    private readonly IVoiceEntryRepository _voiceEntryRepository;
    private readonly IParsedTaskItemRepository _parsedTaskItemRepository;
    private readonly IAudioStorageService _audioStorageService;
    private readonly ITranscriptionService _transcriptionService;
    private readonly IItemClassifierService _itemClassifierService;

    public ProcessVoiceEntryTranscriptionUseCase(
        IVoiceEntryRepository voiceEntryRepository,
        IParsedTaskItemRepository parsedTaskItemRepository,
        IAudioStorageService audioStorageService,
        ITranscriptionService transcriptionService,
        IItemClassifierService itemClassifierService)
    {
        _voiceEntryRepository = voiceEntryRepository;
        _parsedTaskItemRepository = parsedTaskItemRepository;
        _audioStorageService = audioStorageService;
        _transcriptionService = transcriptionService;
        _itemClassifierService = itemClassifierService;
    }

    public async Task ExecuteAsync(Guid voiceEntryId, CancellationToken cancellationToken = default)
    {
        var voiceEntry = await _voiceEntryRepository.GetByIdAsync(voiceEntryId, cancellationToken);
        if (voiceEntry == null)
        {
            throw new KeyNotFoundException($"Captura de voz com ID {voiceEntryId} não foi encontrada.");
        }

        try
        {
            // 1. Marca como em processamento
            voiceEntry.MarkAsProcessing();
            await _voiceEntryRepository.UpdateAsync(voiceEntry, cancellationToken);

            // 2. Executa a Transcrição (Speech-To-Text)
            using var audioStream = new MemoryStream();
            // Para simplificar a demonstração, se o stream local puder ser aberto via IAudioStorageService ou File
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), voiceEntry.AudioFilePath);
            if (File.Exists(fullPath))
            {
                using var fileStream = File.OpenRead(fullPath);
                await fileStream.CopyToAsync(audioStream, cancellationToken);
            }
            audioStream.Position = 0;

            var transcriptionResult = await _transcriptionService.TranscribeAudioAsync(
                audioStream,
                voiceEntry.AudioFormat,
                cancellationToken);

            // 3. Marca como concluído com o texto transcrito
            voiceEntry.MarkAsCompleted(transcriptionResult.Text);
            await _voiceEntryRepository.UpdateAsync(voiceEntry, cancellationToken);

            // 4. Executa a Classificação / Parsing de Tarefas
            var taskDrafts = await _itemClassifierService.ClassifyAndExtractItemsAsync(
                transcriptionResult.Text,
                DateTime.UtcNow,
                cancellationToken);

            var parsedItems = taskDrafts.Select(draft => ParsedTaskItem.Create(
                voiceEntry.Id,
                voiceEntry.UserId,
                draft.Title,
                draft.Category,
                draft.Priority,
                draft.DueDate,
                draft.ConfidenceScore * transcriptionResult.ConfidenceScore
            )).ToList();

            if (parsedItems.Any())
            {
                await _parsedTaskItemRepository.AddRangeAsync(parsedItems, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            voiceEntry.MarkAsFailed(ex.Message);
            await _voiceEntryRepository.UpdateAsync(voiceEntry, cancellationToken);
            throw;
        }
    }
}
