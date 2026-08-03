using Ecommerce.Application.Authentication;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace Ecommerce.Api.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/register", async (
            RegisterUserRequest request,
            IValidator<RegisterUserRequest> validator,
            [FromServices] RegisterUserHandler handler,
            CancellationToken cancellationToken) =>
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var response = await handler.HandleAsync(request, cancellationToken);

            return response is null
                ? Results.Conflict("User email already exists.")
                : Results.Created($"/api/v1/users/{response.Id}", response);
        });

        app.MapPost("/auth/login", async (
            LoginRequest request,
            IValidator<LoginRequest> validator,
            [FromServices] LoginHandler handler,
            CancellationToken cancellationToken) =>
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var response = await handler.HandleAsync(request, cancellationToken);

            return response is null
                ? Results.Unauthorized()
                : Results.Ok(response);
        });

        return app;
    }
}