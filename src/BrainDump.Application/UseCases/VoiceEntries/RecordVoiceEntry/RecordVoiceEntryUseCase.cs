using BrainDump.Application.Common.Interfaces;
using BrainDump.Application.DTOs.Voice;
using BrainDump.Domain.Entities;
using BrainDump.Domain.Repositories;

namespace BrainDump.Application.UseCases.VoiceEntries.RecordVoiceEntry;

/// <summary>
/// Caso de uso responsável pelo recebimento, armazenamento e criação da gravação de voz.
/// </summary>
public class RecordVoiceEntryUseCase
{
    private const long MaxFileSizeBytes = 10 * 1024 * 1024; // 10 MB
    private const int MaxDurationSeconds = 300; // 5 minutos

    private readonly IAudioStorageService _audioStorageService;
    private readonly IVoiceEntryRepository _voiceEntryRepository;

    public RecordVoiceEntryUseCase(
        IAudioStorageService audioStorageService,
        IVoiceEntryRepository voiceEntryRepository)
    {
        _audioStorageService = audioStorageService;
        _voiceEntryRepository = voiceEntryRepository;
    }

    public async Task<VoiceEntryResponse> ExecuteAsync(
        RecordVoiceEntryCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.UserId == Guid.Empty)
            throw new ArgumentException("ID do usuário inválido.", nameof(command.UserId));

        if (command.AudioStream == null || command.ByteSize <= 0)
            throw new ArgumentException("O arquivo de áudio não pode estar vazio.", nameof(command.AudioStream));

        if (command.ByteSize > MaxFileSizeBytes)
            throw new InvalidOperationException($"O tamanho do arquivo excede o limite máximo permitido de 10 MB.");

        if (command.DurationSeconds > MaxDurationSeconds)
            throw new InvalidOperationException("A gravação excede a duração máxima permitida de 5 minutos.");

        var fileExtension = Path.GetExtension(command.FileName).TrimStart('.').ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(fileExtension))
        {
            fileExtension = "m4a"; // fallback default
        }

        var uniqueFileName = $"{Guid.NewGuid()}_{DateTime.UtcNow:yyyyMMddHHmmss}.{fileExtension}";

        // Salva o stream de áudio no serviço de armazenamento
        var storedFilePath = await _audioStorageService.SaveAudioAsync(command.AudioStream, uniqueFileName, cancellationToken);

        // Cria a entidade de domínio
        var voiceEntry = VoiceEntry.Create(
            command.UserId,
            storedFilePath,
            fileExtension,
            command.DurationSeconds,
            command.ByteSize);

        // Persiste no banco de dados
        await _voiceEntryRepository.AddAsync(voiceEntry, cancellationToken);

        return new VoiceEntryResponse(
            voiceEntry.Id,
            voiceEntry.UserId,
            voiceEntry.AudioFilePath,
            voiceEntry.AudioFormat,
            voiceEntry.DurationSeconds,
            voiceEntry.ByteSize,
            voiceEntry.Status,
            voiceEntry.TranscribedText,
            voiceEntry.CreatedAt);
    }
}
