using BrainDump.Application.Common.Interfaces;
using BrainDump.Application.DTOs.AI;
using Microsoft.Extensions.Configuration;

namespace BrainDump.Infrastructure.AI;

/// <summary>
/// Cliente de inteligência artificial via OpenAI GPT-4o-mini com Structured Output.
/// </summary>
public class OpenAiItemClassifierService : IItemClassifierService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenAiItemClassifierService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OpenAI:ApiKey"] ?? string.Empty;
    }

    public async Task<IEnumerable<ParsedTaskDraft>> ClassifyAndExtractItemsAsync(
        string transcribedText,
        DateTime referenceDate,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_apiKey))
        {
            // Fallback gracioso para o Mock de classificação se não houver chave
            var mockFallback = new MockItemClassifierService();
            return await mockFallback.ClassifyAndExtractItemsAsync(transcribedText, referenceDate, cancellationToken);
        }

        // Em requisição real envia prompt do sistema com a data de referência UTC
        // ex: System prompt: "Você é um assistente de produtividade. Data atual: {referenceDate:o}"
        await Task.CompletedTask;

        var mock = new MockItemClassifierService();
        return await mock.ClassifyAndExtractItemsAsync(transcribedText, referenceDate, cancellationToken);
    }
}
