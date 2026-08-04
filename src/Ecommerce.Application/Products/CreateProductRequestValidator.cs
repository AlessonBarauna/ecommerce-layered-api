using FluentValidation;

namespace Ecommerce.Application.Products;

public sealed class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(request => request.CategoryId)
            .NotEmpty();

        RuleFor(request => request.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200);

        RuleFor(request => request.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(request => request.Price)
            .GreaterThan(0);

        RuleFor(request => request.StockQuantity)
            .GreaterThanOrEqualTo(0);
    }
}