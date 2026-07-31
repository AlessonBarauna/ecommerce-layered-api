using Ecommerce.Application.Abstractions;

namespace Ecommerce.Application.Customers;

public sealed class GetCustomerByIdHandler
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerResponse?> HandleAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);

        if (customer is null)
        {
            return null;
        }

        return new CustomerResponse(
            customer.Id,
            customer.UserId,
            customer.FullName,
            customer.Email,
            customer.Document,
            customer.IsActive,
            customer.CreatedAt);
    }
}