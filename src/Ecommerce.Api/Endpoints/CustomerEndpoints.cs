using Ecommerce.Application.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Endpoints;

public static class CustomerEndpoints
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/customers", async (
            CreateCustomerRequest request,
            [FromServices] CreateCustomerHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(request, cancellationToken);

            return response is null
                ? Results.Conflict("Customer email already exists.")
                : Results.Created($"/api/v1/customers/{response.Id}", response);
        })
        .RequireAuthorization();

        app.MapGet("/customers/{id:guid}", async (
            Guid id,
            [FromServices] GetCustomerByIdHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(id, cancellationToken);

            return response is null
                ? Results.NotFound()
                : Results.Ok(response);
        })
        .RequireAuthorization();

        return app;
    }
}