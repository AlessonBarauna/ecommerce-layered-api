namespace Ecommerce.Application.Carts;

public sealed record CartItemResponse(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal Subtotal);