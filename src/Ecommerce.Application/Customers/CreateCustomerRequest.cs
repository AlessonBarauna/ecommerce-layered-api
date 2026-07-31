namespace Ecommerce.Application.Customers;

public sealed record CreateCustomerRequest(
    Guid UserId,
    string FullName,
    string Email,
    string Document
);