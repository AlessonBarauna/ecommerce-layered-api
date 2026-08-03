using System.Security.Claims;
using Ecommerce.Application.Abstractions;

namespace Ecommerce.Api.Services;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?
            .User
            .FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.TryParse(userId, out var parsedUserId)
            ? parsedUserId
            : null;
        }
    }

    public string? Email => 
    _httpContextAccessor.HttpContext?
        .User
        .FindFirstValue(ClaimTypes.Email);

    public string? Role => 
    _httpContextAccessor.HttpContext?
        .User
        .FindFirstValue(ClaimTypes.Role);
}