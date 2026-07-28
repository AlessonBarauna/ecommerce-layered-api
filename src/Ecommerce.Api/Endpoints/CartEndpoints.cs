using Ecommerce.Application.Carts;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Endpoints;

public static class CartEndpoints
{
    public static IEndpointRouteBuilder MapCartEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/customers/{customerId:guid}/cart/items", async (
            Guid customerId,
            AddItemToCartRequest request,
            [FromServices] AddItemToCartHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(customerId, request, cancellationToken);

            return response is null
                ? Results.BadRequest("Customer or product was not found.")
                : Results.Ok(response);
        });

        app.MapGet("/customers/{customerId:guid}/cart", async (
            Guid customerId,
            [FromServices] GetCartByCustomerIdHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(customerId, cancellationToken);

            return response is null
                ? Results.NotFound()
                : Results.Ok(response);
        });

        return app;
    }
}