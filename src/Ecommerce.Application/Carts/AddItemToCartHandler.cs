using Ecommerce.Application.Abstractions;
using Ecommerce.Domain.Carts;

namespace Ecommerce.Application.Carts;

public sealed class AddItemToCartHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddItemToCartHandler(
        ICustomerRepository customerRepository,
        IProductRepository productRepository,
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _cartRepository = cartRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CartResponse?> HandleAsync(
        Guid customerId,
        AddItemToCartRequest request,
        CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId, cancellationToken);

        if (customer is null)
        {
            return null;
        }

        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null || !product.IsActive)
        {
            return null;
        }

        if (request.Quantity > product.StockQuantity)
        {
            throw new InvalidOperationException("Insufficient stock.");
        }

        var cart = await _cartRepository.GetByCustomerIdAsync(customerId, cancellationToken);

        if (cart is null)
        {
            cart = new Cart(Guid.NewGuid(), customerId);

            await _cartRepository.AddAsync(cart, cancellationToken);
        }

        cart.AddItem(
            product.Id,
            product.Name,
            request.Quantity,
            product.Price);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ToResponse(cart);
    }

    private static CartResponse ToResponse(Cart cart)
    {
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