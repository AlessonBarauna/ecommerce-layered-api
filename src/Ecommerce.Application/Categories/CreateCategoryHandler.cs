using Ecommerce.Application.Abstractions;
using Ecommerce.Domain.Categories;

namespace Ecommerce.Application.Categories;

public sealed class CreateCategoryHandler
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryHandler(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork)

    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryResponse> HandleAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var category = new Category(
            Guid.NewGuid(),
            request.Name,
            request.Description);

        await _categoryRepository.AddAsync(category, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CategoryResponse(
            category.Id,
            category.Name,
            category.Description,
            category.IsActive,
            category.CreatedAt);
    }
}