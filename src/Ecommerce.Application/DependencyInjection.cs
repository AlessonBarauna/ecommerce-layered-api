using Ecommerce.Application.Categories;
using Ecommerce.Application.Products;
using Microsoft.Extensions.DependencyInjection;
using Ecommerce.Application.Carts;
using Ecommerce.Application.Orders;
using Ecommerce.Application.Customers;
using Ecommerce.Application.Authentication;

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
        services.AddScoped<CreateCustomerHandler>();
        services.AddScoped<GetCustomerByIdHandler>();

        services.AddScoped<AddItemToCartHandler>();
        services.AddScoped<GetCartByCustomerIdHandler>();
        services.AddScoped<CheckoutHandler>();
        services.AddScoped<LoginHandler>();
        services.AddScoped<RegisterUserHandler>();
        services.AddScoped<GetCurrentCustomerCartHandler>();
        services.AddScoped<AddItemToCurrentCustomerCartHandler>();

        return services;
    }
}