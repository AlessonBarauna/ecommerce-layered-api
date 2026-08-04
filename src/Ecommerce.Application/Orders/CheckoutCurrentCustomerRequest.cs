namespace Ecommerce.Application.Orders;

public sealed record CheckoutCurrentCustomerRequest(
    decimal ShippingAmount,
    decimal DiscountAmount);