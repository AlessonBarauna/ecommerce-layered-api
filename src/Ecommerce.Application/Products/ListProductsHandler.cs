using Ecommerce.Application.Abstractions;

namespace Ecommerce.Application.Products;

public sealed class ListProductsHandler
{
    private readonly IProductRepository _productRepository;

    public ListProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IReadOnlyCollection<ProductResponse>> HandleAsync(
        CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);

        return products
            .Select(product => new ProductResponse(
                product.Id,
                product.CategoryId,
                product.Name,
                product.Description,
                product.Price,
                product.StockQuantity,
                product.IsActive,
                product.CreatedAt))
            .ToList();
    }
}