using Ecommerce.Application.Abstractions;

namespace Ecommerce.Application.Categories;

public sealed class GetCategoryByIdHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryResponse?> HandleAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        if (category is null)
        {
            return null;
        }

        return new CategoryResponse(
            category.Id,
            category.Name,
            category.Description,
            category.IsActive,
            category.CreatedAt);
    }
}