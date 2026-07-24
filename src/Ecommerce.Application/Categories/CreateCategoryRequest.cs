namespace Ecommerce.Application.Categories;

public sealed record CreateCategoryRequest(
    string Name,
    string Description);