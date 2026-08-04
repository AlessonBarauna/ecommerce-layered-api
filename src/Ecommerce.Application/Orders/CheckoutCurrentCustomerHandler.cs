using Ecommerce.Application.Abstractions;
using Ecommerce.Domain.Orders;

namespace Ecommerce.Application.Orders;

public sealed class CheckoutCurrentCustomerHandler
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ICustomerRepository _customerRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CheckoutCurrentCustomerHandler(
        ICurrentUserService currentUserService,
        ICustomerRepository customerRepository,
        ICartRepository cartRepository,
        IProductRepository productRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _customerRepository = customerRepository;
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderResponse?> HandleAsync(
        CheckoutCurrentCustomerRequest request,
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

        var cart = await _cartRepository.GetByCustomerIdAsync(
            customer.Id,
            cancellationToken);

        if (cart is null || cart.Items.Count == 0)
        {
            return null;
        }

        var orderItems = new List<OrderItem>();

        foreach (var cartItem in cart.Items)
        {
            var product = await _productRepository.GetByIdAsync(
                cartItem.ProductId,
                cancellationToken);

            if (product is null || !product.IsActive)
            {
                return null;
            }

            product.DecreaseStock(cartItem.Quantity);

            orderItems.Add(new OrderItem(
                product.Id,
                product.Name,
                cartItem.Quantity,
                product.Price));
        }

        var order = new Order(
            Guid.NewGuid(),
            customer.Id,
            orderItems,
            request.ShippingAmount,
            request.DiscountAmount);

        await _orderRepository.AddAsync(order, cancellationToken);

        cart.Clear();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var items = order.Items
            .Select(item => new OrderItemResponse(
                item.ProductId,
                item.ProductName,
                item.Quantity,
                item.UnitPrice,
                item.Subtotal))
            .ToList();

        return new OrderResponse(
            order.Id,
            order.CustomerId,
            items,
            order.Subtotal,
            order.ShippingAmount,
            order.DiscountAmount,
            order.Total,
            order.Status,
            order.CreatedAt,
            order.UpdatedAt);
    }
}