using FluentValidation;

namespace Ecommerce.Application.Customers;

public sealed class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator()
    {
        RuleFor(request => request.UserId)
            .NotEmpty();

        RuleFor(request => request.FullName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200);

        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(request => request.Document)
            .NotEmpty()
            .MaximumLength(30);
    }
}