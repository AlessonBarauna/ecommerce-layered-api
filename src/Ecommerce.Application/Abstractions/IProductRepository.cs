using Ecommerce.Domain.Products;

namespace Ecommerce.Application.Abstractions;

public interface IProductRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken);

    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Product>> GetAllAsync(CancellationToken cancellationToken);
}