namespace Ecommerce.Application.Authentication;

public sealed record RegisterUserRequest(
    string FullName,
    string Email,
    string Password
);