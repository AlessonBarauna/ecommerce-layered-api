namespace Ecommerce.Application.Products;

public sealed record CreateProductRequest(
    Guid CategoryId,
    string Name,
    string Description,
    decimal Price,
    int StockQuantity);