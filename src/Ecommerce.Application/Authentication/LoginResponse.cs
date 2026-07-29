namespace Ecommerce.Application.Authentication;

public sealed record LoginResponse(
    string AcessToken,
    DateTime ExpireAt
);