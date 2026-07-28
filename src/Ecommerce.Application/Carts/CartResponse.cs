namespace Ecommerce.Application.Carts;

public sealed record CartResponse(
    Guid Id,
    Guid CustomerId,
    IReadOnlyCollection<CartItemResponse> Items,
    decimal Total,
    DateTime UpdatedAt);