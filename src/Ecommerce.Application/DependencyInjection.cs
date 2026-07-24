using Ecommerce.Application.Categories;
using Ecommerce.Application.Products;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateCategoryHandler>();
        services.AddScoped<ListCategoriesHandler>();
        services.AddScoped<GetCategoryByIdHandler>();

        services.AddScoped<CreateProductHandler>();
        services.AddScoped<ListProductsHandler>();
        services.AddScoped<GetProductByIdHandler>();

        return services;
    }
}