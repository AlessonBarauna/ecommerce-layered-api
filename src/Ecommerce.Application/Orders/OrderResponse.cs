using Ecommerce.Domain.Orders;

namespace Ecommerce.Application.Orders;

public sealed record OrderResponse(
    Guid Id,
    Guid CustomerId,
    IReadOnlyCollection<OrderItemResponse> Items,
    decimal Subtotal,
    decimal ShippingAmount,
    decimal DiscountAmount,
    decimal Total,
    OrderStatus Status,
    DateTime CreatedAt);