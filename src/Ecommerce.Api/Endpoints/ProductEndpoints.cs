using Ecommerce.Application.Products;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Ecommerce.Api.Errors;

namespace Ecommerce.Api.Endpoints;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (
            CreateProductRequest request,
            IValidator<CreateProductRequest> validator,
            [FromServices] CreateProductHandler handler ,
            CancellationToken cancellationToken) => 
            
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var response = await handler.HandleAsync(request, cancellationToken);

            return response is null 
                ? ApiErrors.BadRequest("Category was not found.")
                : Results.Created($"/api/v1/products/{response.Id}", response);
        })
        .RequireAuthorization("AdminOnly");

        app.MapGet("/products", async (
            [FromServices] ListProductsHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(cancellationToken);

            return Results.Ok(response);
        });

        app.MapGet("/products/{id:guid}", async (
            Guid id,
            [FromServices] GetProductByIdHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(id, cancellationToken);

            return response is null
                ? ApiErrors.NotFound("Product was not found.")
                : Results.Ok(response);
        });

        return app;
    }
}