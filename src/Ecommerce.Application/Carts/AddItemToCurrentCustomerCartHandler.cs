using Ecommerce.Application.Abstractions;
using Ecommerce.Domain.Carts;

namespace Ecommerce.Application.Carts;

public sealed class AddItemToCurrentCustomerCartHandler
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddItemToCurrentCustomerCartHandler(
        ICurrentUserService currentUserService,
        ICustomerRepository customerRepository,
        IProductRepository productRepository,
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _cartRepository = cartRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CartResponse?> HandleAsync(
        AddItemToCartRequest request,
        CancellationToken cancellationToken)
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

        var product = await _productRepository.GetByIdAsync(
            request.ProductId,
            cancellationToken);

        if (product is null || !product.IsActive)
        {
            return null;
        }

        if (product.StockQuantity < request.Quantity)
        {
            return null;
        }

        var cart = await _cartRepository.GetByCustomerIdAsync(
            customer.Id,
            cancellationToken);

        if (cart is null)
        {
            cart = new Cart(Guid.NewGuid(), customer.Id);

            await _cartRepository.AddAsync(cart, cancellationToken);
        }

        cart.AddItem(
            product.Id,
            product.Name,
            request.Quantity,
            product.Price);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

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