namespace Ecommerce.Api.Endpoints;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        var apiv1 = app.MapGroup("/api/v1");

        apiv1.MapCategoryEndpoints();
        apiv1.MapProductEndpoints();

        return app;
    }
}