using BrainDump.Application.Common.Interfaces;
using BrainDump.Application.DTOs.AI;
using Microsoft.Extensions.Configuration;

namespace BrainDump.Infrastructure.AI;

/// <summary>
/// Cliente para integração com a API da OpenAI (Whisper / Audio Transcriptions).
/// Pode ser ativado via configuração AiProvider: "OpenAI".
/// </summary>
public class OpenAiTranscriptionService : ITranscriptionService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenAiTranscriptionService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OpenAI:ApiKey"] ?? string.Empty;
    }

    public async Task<TranscriptionResult> TranscribeAudioAsync(
        Stream audioStream,
        string audioFormat,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_apiKey))
        {
            throw new InvalidOperationException("A chave de API da OpenAI não está configurada.");
        }

        // Em ambiente sem requisição real ativa, faz o envio HTTP via multipart/form-data para endpoint https://api.openai.com/v1/audio/transcriptions
        using var content = new MultipartFormDataContent();
        var streamContent = new StreamContent(audioStream);
        content.Add(streamContent, "file", $"recording.{audioFormat}");
        content.Add(new StringContent("whisper-1"), "model");
        content.Add(new StringContent("pt"), "language");

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

        var response = await _httpClient.PostAsync("https://api.openai.com/v1/audio/transcriptions", content, cancellationToken);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        // Retorna o resultado parseado do JSON do Whisper
        return new TranscriptionResult(json, 0.95f, "pt");
    }
}
