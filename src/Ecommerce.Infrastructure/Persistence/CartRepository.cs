using Ecommerce.Application.Abstractions;
using Ecommerce.Domain.Carts;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Persistence;

public sealed class CartRepository : ICartRepository
{
    private readonly AppDbContext _dbContext;

    public CartRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        Cart cart,
        CancellationToken cancellationToken)
    {
        await _dbContext.Carts.AddAsync(cart, cancellationToken);
    }

    public async Task<Cart?> GetByCustomerIdAsync(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Carts
            .FirstOrDefaultAsync(cart => cart.CustomerId == customerId, cancellationToken);
    }
}