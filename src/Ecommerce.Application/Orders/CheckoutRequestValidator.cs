using FluentValidation;

namespace Ecommerce.Application.Orders;

public sealed class CheckoutRequestValidator : AbstractValidator<CheckoutRequest>
{
    public CheckoutRequestValidator()
    {
        RuleFor(request => request.CustomerId)
            .NotEmpty();

        RuleFor(request => request.ShippingAmount)
            .GreaterThanOrEqualTo(0);

        RuleFor(request => request.DiscountAmount)
            .GreaterThanOrEqualTo(0);
    }
}