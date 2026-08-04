using FluentValidation;

namespace Ecommerce.Application.Categories;

public sealed class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(150);

        RuleFor(request => request.Description)
            .NotEmpty()
            .MaximumLength(500);
    }
}