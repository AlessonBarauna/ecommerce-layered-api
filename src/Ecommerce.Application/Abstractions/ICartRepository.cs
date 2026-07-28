using Ecommerce.Domain.Carts;

namespace Ecommerce.Application.Abstractions;

public interface ICartRepository
{
    Task AddAsync(Cart cart, CancellationToken cancellationToken);
    Task<Cart?> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken);
}