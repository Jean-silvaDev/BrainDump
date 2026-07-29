using BrainDump.Application.DTOs.Auth;
using BrainDump.Application.UseCases.Auth;

namespace BrainDump.Web.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth").WithTags("Autenticação");

        group.MapPost("/register", async (RegisterRequest request, RegisterUserUseCase useCase, CancellationToken ct) =>
        {
            try
            {
                var result = await useCase.ExecuteAsync(request, ct);
                return Results.Created($"/api/users/{result.Id}", result);
            }
            catch (InvalidOperationException ex)
            {
                return Results.Conflict(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        })
        .WithName("RegisterUser")
        .WithSummary("Registra um novo usuário no sistema");

        group.MapPost("/login", async (LoginRequest request, LoginUseCase useCase, CancellationToken ct) =>
        {
            try
            {
                var result = await useCase.ExecuteAsync(request, ct);
                return Results.Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Results.Unauthorized();
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        })
        .WithName("LoginUser")
        .WithSummary("Realiza login de usuário e retorna tokens JWT");

        group.MapPost("/refresh", async (RefreshTokenRequest request, RefreshTokenUseCase useCase, CancellationToken ct) =>
        {
            try
            {
                var result = await useCase.ExecuteAsync(request, ct);
                return Results.Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Results.Unauthorized();
            }
        })
        .WithName("RefreshToken")
        .WithSummary("Renova o Access Token via Refresh Token");
    }
}
