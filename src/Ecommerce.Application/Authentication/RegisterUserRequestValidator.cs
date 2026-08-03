using FluentValidation;

namespace Ecommerce.Application.Authentication;

public sealed class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(request => request.FullName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200);

        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(200);

        RuleFor(request => request.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(100);
    }
}