using Ecommerce.Domain.Categories;

namespace Ecommerce.Application.Abstractions;

public interface ICategoryRepository
{
    Task AddAsync(Category category, CancellationToken cancellationToken);

    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Category>> GetAllAsync(CancellationToken cancellationToken);
}