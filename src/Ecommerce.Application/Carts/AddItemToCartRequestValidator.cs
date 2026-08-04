using FluentValidation;

namespace Ecommerce.Application.Carts;

public sealed class AddItemToCartRequestValidator : AbstractValidator<AddItemToCartRequest>
{
    public AddItemToCartRequestValidator()
    {
        RuleFor(request => request.ProductId)
            .NotEmpty();

        RuleFor(request => request.Quantity)
            .GreaterThan(0);
    }
}