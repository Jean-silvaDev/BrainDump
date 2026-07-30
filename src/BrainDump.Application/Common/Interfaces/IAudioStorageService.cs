namespace BrainDump.Application.Common.Interfaces;

/// <summary>
/// Contrato para serviço de armazenamento de arquivos de áudio.
/// </summary>
public interface IAudioStorageService
{
    /// <summary>
    /// Salva o stream de áudio no sistema de armazenamento.
    /// </summary>
    /// <param name="audioStream">Stream contendo os dados do áudio.</param>
    /// <param name="fileName">Nome do arquivo com extensão.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Caminho relativo do arquivo armazenado.</returns>
    Task<string> SaveAudioAsync(Stream audioStream, string fileName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove um arquivo de áudio do sistema de armazenamento.
    /// </summary>
    Task DeleteAudioAsync(string filePath, CancellationToken cancellationToken = default);
}
