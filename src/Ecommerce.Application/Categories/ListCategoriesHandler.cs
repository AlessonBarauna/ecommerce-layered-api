using Ecommerce.Application.Abstractions;
using Ecommerce.Domain.Categories;

namespace Ecommerce.Application.Categories;

public sealed class ListCategoriesHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public ListCategoriesHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IReadOnlyCollection<CategoryResponse>> HandleAsync(
        CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync(cancellationToken);

        return categories
            .Select(category => new CategoryResponse(
                category.Id,
                category.Name,
                category.Description,
                category.IsActive,
                category.CreatedAt))
            .ToList();
    }
}
