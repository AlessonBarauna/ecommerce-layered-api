using Ecommerce.Application.Categories;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace Ecommerce.Api.Endpoints;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/categories", async (
            CreateCategoryRequest request,
            IValidator<CreateCategoryRequest> validator,
            [FromServices] CreateCategoryHandler handler,
            CancellationToken cancellationToken) =>
        {

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }
            var response = await handler.HandleAsync(request, cancellationToken);

            return Results.Created($"/api/v1/categories/{response.Id}", response);
        })
        .RequireAuthorization("AdminOnly");

        app.MapGet("/categories", async (
            [FromServices] ListCategoriesHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(cancellationToken);

            return Results.Ok(response);
        });

        app.MapGet("/categories/{id:guid}", async (
            Guid id,
            [FromServices] GetCategoryByIdHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(id, cancellationToken);

            return response is null
                ? Results.NotFound()
                : Results.Ok(response);
        });

        return app;
    }
}