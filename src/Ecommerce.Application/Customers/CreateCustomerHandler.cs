using System.ComponentModel;
using Ecommerce.Application.Abstractions;
using Ecommerce.Domain.Customers;

namespace Ecommerce.Application.Customers;

public sealed class CreateCustomerHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerHandler(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerResponse?> HandleAsync(
        CreateCustomerRequest  request,
        CancellationToken cancellationToken)
    {
        var existingCustomer = await _customerRepository.GetByEmailAsync(
            request.Email,
            cancellationToken);

        if (existingCustomer is not null)
        {
            return null;
        }

        var customer = new Customer(
            Guid.NewGuid(),
            request.FullName,
            request.Email,
            request.Document);

        await _customerRepository.AddAsync(customer, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CustomerResponse(
            customer.Id,
            customer.FullName,
            customer.Email,
            customer.Document,
            customer.IsActive,
            customer.CreatedAt);
    }
    
}