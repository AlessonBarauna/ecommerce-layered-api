namespace Ecommerce.Api.Errors;

public static class ApiErrors
{
    public static IResult BadRequest(string detail)
    {
        return Results.Problem(
            statusCode: StatusCodes.Status400BadRequest,
            title: "Bad Request",
            detail: detail,
            type: "https://httpstatuses.com/400"
        );
    }

    public static IResult Unauthorized()
    {
        return Results.Problem(
            statusCode: StatusCodes.Status401Unauthorized,
            title: "Unauthorized",
            detail: "Authentication is required to access this resource.",
            type: "https://httpstatuses.com/401"
        );
    }
    public static IResult Forbidden()
    {
        return Results.Problem(
            statusCode: StatusCodes.Status403Forbidden,
            title: "Forbidden",
            detail: "You do not have permission to access this resource.",
            type: "https://httpstatuses.com/403");
    }

    public static IResult NotFound(string detail = "Resource was not found.")
    {
        return Results.Problem(
            statusCode: StatusCodes.Status404NotFound,
            title: "Not Found",
            detail: detail,
            type: "https://httpstatuses.com/404");
    }

    public static IResult Conflict(string detail)
    {
        return Results.Problem(
            statusCode: StatusCodes.Status409Conflict,
            title: "Conflict",
            detail: detail,
            type: "https://httpstatuses.com/409");
    }
}