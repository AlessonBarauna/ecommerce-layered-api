namespace Ecommerce.Application.Customers;

public sealed record CustomerResponse(
    Guid Id,
    string FullName,
    string Email,
    string Document,
    bool IsActive,
    DateTime CreatedAt
);