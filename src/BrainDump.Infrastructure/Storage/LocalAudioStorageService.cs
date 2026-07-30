using BrainDump.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BrainDump.Infrastructure.Storage;

/// <summary>
/// Implementação do serviço de armazenamento salvando arquivos em pasta local no disco.
/// </summary>
public class LocalAudioStorageService : IAudioStorageService
{
    private readonly string _storagePath;

    public LocalAudioStorageService(IConfiguration configuration)
    {
        var configuredPath = configuration["AudioStorage:Path"];
        _storagePath = !string.IsNullOrWhiteSpace(configuredPath)
            ? configuredPath
            : Path.Combine(Directory.GetCurrentDirectory(), "uploads", "voice-entries");

        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
        }
    }

    public async Task<string> SaveAudioAsync(Stream audioStream, string fileName, CancellationToken cancellationToken = default)
    {
        if (audioStream == null)
            throw new ArgumentNullException(nameof(audioStream));

        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("Nome do arquivo não fornecido.", nameof(fileName));

        var fullPath = Path.Combine(_storagePath, fileName);
        
        using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
        {
            if (audioStream.CanSeek)
            {
                audioStream.Position = 0;
            }
            await audioStream.CopyToAsync(fileStream, cancellationToken);
        }

        var relativePath = Path.Combine("uploads", "voice-entries", fileName).Replace('\\', '/');
        return relativePath;
    }

    public Task DeleteAudioAsync(string filePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return Task.CompletedTask;

        var fileName = Path.GetFileName(filePath);
        var fullPath = Path.Combine(_storagePath, fileName);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }
}
