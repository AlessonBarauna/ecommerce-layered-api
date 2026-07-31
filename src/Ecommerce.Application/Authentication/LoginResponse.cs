namespace Ecommerce.Application.Authentication;

public sealed record LoginResponse(
    string AccessToken,
    DateTime ExpiresAt
);