namespace Ecommerce.Application.Products;

public sealed record ProductResponse(
    Guid Id,
    Guid CategoryId,
    string Name,
    string Description,
    decimal Price,
    int StockQuantity,
    bool IsActive,
    DateTime CreatedAt);