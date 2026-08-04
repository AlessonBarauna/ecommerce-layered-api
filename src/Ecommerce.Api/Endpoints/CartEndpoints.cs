using Ecommerce.Application.Carts;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Ecommerce.Api.Errors;

namespace Ecommerce.Api.Endpoints;

public static class CartEndpoints
{
    public static IEndpointRouteBuilder MapCartEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/customers/{customerId:guid}/cart/items", async (
            Guid customerId,
            AddItemToCartRequest request,
            IValidator<AddItemToCartRequest> validator,
            [FromServices] AddItemToCartHandler handler,
            CancellationToken cancellationToken) =>
        {

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var response = await handler.HandleAsync(customerId, request, cancellationToken);

            return response is null
                ? ApiErrors.BadRequest("Customer or product was not found.")
                : Results.Ok(response);
        })
        .RequireAuthorization("CustomerOnly");

        app.MapGet("/customers/{customerId:guid}/cart", async (
            Guid customerId,
            [FromServices] GetCartByCustomerIdHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(customerId, cancellationToken);

            return response is null
                ? ApiErrors.NotFound("Cart was not found.")
                : Results.Ok(response);
        })
        .RequireAuthorization("CustomerOnly");

        app.MapPost("/me/cart/items", async (
            AddItemToCartRequest request,
            IValidator<AddItemToCartRequest> validator,
            [FromServices] AddItemToCurrentCustomerCartHandler handler,
            CancellationToken cancellationToken) =>
        {

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }
            
            var response = await handler.HandleAsync(request, cancellationToken);

            return response is null
                ? ApiErrors.BadRequest("Customer or product was not found.")
                : Results.Ok(response);
        })
        .RequireAuthorization("CustomerOnly");

        app.MapGet("/me/cart", async (
            [FromServices] GetCurrentCustomerCartHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(cancellationToken);

            return response is null
                ? ApiErrors.NotFound("Cart was not found.")
                : Results.Ok(response);
        })
        .RequireAuthorization("CustomerOnly");

        return app;
    }
}