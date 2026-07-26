using Ecommerce.Application.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace Ecommerce.Api.Endpoints;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (
            CreateProductRequest request,
            [FromServices] CreateProductHandler handler ,
            CancellationToken cancellationToken) => 
            
        {
            var response = await handler.HandleAsync(request, cancellationToken);

            return response is null 
                ? Results.BadRequest("Category was not found")
                : Results.Created($"/api/v1/products/{response.Id}", response);
        });

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
                ? Results.NotFound()
                : Results.Ok(response);
        });

        return app;
    }
}