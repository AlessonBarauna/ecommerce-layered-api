using Ecommerce.Application.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Endpoints;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/orders/checkout", async (
            CheckoutRequest request,
            [FromServices] CheckoutHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(request, cancellationToken);

            return response is null
                ? Results.BadRequest("Customer, cart or product was not found.")
                : Results.Created($"/api/v1/orders/{response.Id}", response);
        })
        .RequireAuthorization("CustomerOnly");

        return app;
    }
}