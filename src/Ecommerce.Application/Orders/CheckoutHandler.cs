using Ecommerce.Application.Abstractions;
using Ecommerce.Domain.Orders;

namespace Ecommerce.Application.Orders;

public sealed class CheckoutHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CheckoutHandler(
        ICustomerRepository customerRepository,
        ICartRepository cartRepository,
        IProductRepository productRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderResponse?> HandleAsync(
        CheckoutRequest request,
        CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(
            request.CustomerId,
            cancellationToken);

        if (customer is null)
        {
            return null;
        }

        var cart = await _cartRepository.GetByCustomerIdAsync(
            request.CustomerId,
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

            if (cartItem.Quantity > product.StockQuantity)
            {
                throw new InvalidOperationException("Insufficient stock.");
            }

            product.DecreaseStock(cartItem.Quantity);

            orderItems.Add(new OrderItem(
                product.Id,
                product.Name,
                cartItem.Quantity,
                cartItem.UnitPrice));
        }

        var order = new Order(
            Guid.NewGuid(),
            request.CustomerId,
            orderItems,
            request.ShippingAmount,
            request.DiscountAmount);

        await _orderRepository.AddAsync(order, cancellationToken);

        cart.Clear();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ToResponse(order);
    }

    private static OrderResponse ToResponse(Order order)
    {
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
            order.CreatedAt);
    }
}