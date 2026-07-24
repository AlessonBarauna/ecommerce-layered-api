using Ecommerce.Application.Abstractions;
using Ecommerce.Domain.Products;

namespace Ecommerce.Application.Products;

public sealed class CreateProductHandler
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductHandler(
        ICategoryRepository categoryRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductResponse?> HandleAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(
            request.CategoryId,
            cancellationToken);

        if (category is null)
        {
            return null;
        }

        var product = new Product(
            Guid.NewGuid(),
            request.CategoryId,
            request.Name,
            request.Description,
            request.Price,
            request.StockQuantity);

        await _productRepository.AddAsync(product, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ProductResponse(
            product.Id,
            product.CategoryId,
            product.Name,
            product.Description,
            product.Price,
            product.StockQuantity,
            product.IsActive,
            product.CreatedAt);
    }
}