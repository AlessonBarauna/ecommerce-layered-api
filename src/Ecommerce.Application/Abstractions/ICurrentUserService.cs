namespace Ecommerce.Application.Abstractions;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string? Email { get; }
    string Role { get; }
}