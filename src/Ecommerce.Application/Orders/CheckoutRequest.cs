namespace Ecommerce.Application.Orders;

public sealed record CheckoutRequest(
    Guid CustomerId,
    decimal ShippingAmount,
    decimal DiscountAmount
);