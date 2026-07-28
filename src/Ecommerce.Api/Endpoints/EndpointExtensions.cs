namespace Ecommerce.Api.Endpoints;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        var apiV1 = app.MapGroup("/api/v1");

        apiV1.MapCategoryEndpoints();
        apiV1.MapProductEndpoints();
        apiV1.MapCustomerEndpoints();
        apiV1.MapCartEndpoints();
        apiV1.MapOrderEndpoints();

        return app;
    }
}