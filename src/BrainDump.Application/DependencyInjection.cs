using BrainDump.Application.UseCases.Auth;
using BrainDump.Application.UseCases.VoiceEntries.ProcessVoiceEntryTranscription;
using BrainDump.Application.UseCases.VoiceEntries.RecordVoiceEntry;
using Microsoft.Extensions.DependencyInjection;

namespace BrainDump.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterUserUseCase>();
        services.AddScoped<LoginUseCase>();
        services.AddScoped<RefreshTokenUseCase>();
        services.AddScoped<RecordVoiceEntryUseCase>();
        services.AddScoped<ProcessVoiceEntryTranscriptionUseCase>();

        return services;
    }
}


