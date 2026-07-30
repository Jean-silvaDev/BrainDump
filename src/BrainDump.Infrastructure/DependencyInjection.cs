using BrainDump.Application.Common.Interfaces;
using BrainDump.Infrastructure.Authentication;
using BrainDump.Infrastructure.Persistence;
using BrainDump.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BrainDump.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuração de JWT Settings
        var jwtSettings = new JwtSettings();
        configuration.GetSection(JwtSettings.SectionName).Bind(jwtSettings);
        services.Configure<JwtSettings>(options =>
        {
            options.Secret = jwtSettings.Secret;
            options.Issuer = jwtSettings.Issuer;
            options.Audience = jwtSettings.Audience;
            options.ExpiryMinutes = jwtSettings.ExpiryMinutes;
            options.RefreshTokenExpiryDays = jwtSettings.RefreshTokenExpiryDays;
        });

        // Configuração do DbContext (SQL Server no Docker com fallback para In-Memory se não houver ConnectionString)
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
        else
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("BrainDumpDb"));
        }

        // Injeção de dependências das implementações
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<BrainDump.Domain.Repositories.IVoiceEntryRepository, VoiceEntryRepository>();
        services.AddScoped<BrainDump.Domain.Repositories.IParsedTaskItemRepository, ParsedTaskItemRepository>();
        services.AddSingleton<IAudioStorageService, Storage.LocalAudioStorageService>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        // Serviços de IA (STT e LLM) com suporte a Mock ou OpenAI via configuração
        var aiProvider = configuration["AiProvider"] ?? "Mock";
        services.AddHttpClient();

        if (aiProvider.Equals("OpenAI", StringComparison.OrdinalIgnoreCase))
        {
            services.AddScoped<ITranscriptionService, AI.OpenAiTranscriptionService>();
            services.AddScoped<IItemClassifierService, AI.OpenAiItemClassifierService>();
        }
        else
        {
            services.AddScoped<ITranscriptionService, AI.MockTranscriptionService>();
            services.AddScoped<IItemClassifierService, AI.MockItemClassifierService>();
        }

        // Fila e Serviço em Segundo Plano (BackgroundService)
        services.AddSingleton<BackgroundServices.VoiceProcessingQueue>();
        services.AddHostedService<BackgroundServices.VoiceProcessingBackgroundService>();

        return services;
    }
}


