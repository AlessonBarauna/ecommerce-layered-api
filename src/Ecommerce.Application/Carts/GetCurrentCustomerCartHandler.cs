using Ecommerce.Application.Abstractions;

namespace Ecommerce.Application.Carts;

public sealed class GetCurrentCustomerCartHandler
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ICustomerRepository _customerRepository;
    private readonly ICartRepository _cartRepository;

    public GetCurrentCustomerCartHandler(
        ICurrentUserService currentUserService,
        ICustomerRepository customerRepository,
        ICartRepository cartRepository)
    {
        _currentUserService = currentUserService;
        _customerRepository = customerRepository;
        _cartRepository = cartRepository;
    }

    public async Task<CartResponse?> HandleAsync(CancellationToken cancellationToken)
    {
        if (_currentUserService.UserId is null)
        {
            return null;
        }

        var customer = await _customerRepository.GetByUserIdAsync(
            _currentUserService.UserId.Value,
            cancellationToken);

        if (customer is null)
        {
            return null;
        }

        var cart = await _cartRepository.GetByCustomerIdAsync(
            customer.Id,
            cancellationToken);

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