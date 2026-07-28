namespace Ecommerce.Application.Customers;

public sealed record CreateCustomerRequest(
    string FullName,
    string Email,
    string Document
);