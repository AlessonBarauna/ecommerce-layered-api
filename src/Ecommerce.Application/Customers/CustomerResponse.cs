namespace Ecommerce.Application.Customers;

public sealed record CustomerResponse(
    Guid Id,
    Guid UserId,
    string FullName,
    string Email,
    string Document,
    bool IsActive,
    DateTime CreatedAt
);