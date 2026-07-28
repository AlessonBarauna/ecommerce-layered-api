using Ecommerce.Application.Abstractions;

namespace Ecommerce.Application.Carts;

public sealed class GetCartByCustomerIdHandler
{
    private readonly ICartRepository _cartRepository;

    public GetCartByCustomerIdHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<CartResponse?> HandleAsync(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByCustomerIdAsync(customerId, cancellationToken);

        if (cart is null)
        {
            return null;
        }

        var items = cart.Items
            .Select(item => new CartItemResponse(
                item.ProductId,
                item.ProductName,
                item.Quantity,
                item.UnitPrice,
                item.Subtotal))
            .ToList();

        return new CartResponse(
            cart.Id,
            cart.CustomerId,
            items,
            cart.Total,
            cart.UpdatedAt);
    }
}