using BrainDump.Application.UseCases.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace BrainDump.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterUserUseCase>();
        services.AddScoped<LoginUseCase>();
        services.AddScoped<RefreshTokenUseCase>();

        return services;
    }
}
