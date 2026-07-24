using Ecommerce.Application.Abstractions;

namespace Ecommerce.Application.Products;

public sealed class GetProductByIdHandler
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponse?> HandleAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);

        if (product is null)
        {
            return null;
        }

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