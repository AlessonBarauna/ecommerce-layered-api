using Ecommerce.Domain.Users;

namespace Ecommerce.Application.Authentication;

public sealed record RegisterUserResponse(
    Guid Id,
    string FullName,
    string Email,
    UserRole Role,
    DateTime CreatedAt);