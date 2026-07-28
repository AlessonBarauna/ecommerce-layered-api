namespace Ecommerce.Application.Carts;

public sealed record AddItemToCartRequest(
    Guid ProductId,
    int Quantity);