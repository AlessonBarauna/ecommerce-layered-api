using FluentValidation;

namespace Ecommerce.Application.Orders;

public sealed class CheckoutCurrentCustomerRequestValidator : AbstractValidator<CheckoutCurrentCustomerRequest>
{
    public CheckoutCurrentCustomerRequestValidator()
    {
        RuleFor(request => request.ShippingAmount)
            .GreaterThanOrEqualTo(0);

        RuleFor(request => request.DiscountAmount)
            .GreaterThanOrEqualTo(0);
    }
}