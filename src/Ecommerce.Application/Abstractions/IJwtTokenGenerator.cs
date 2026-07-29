using Ecommerce.Domain.Users;

namespace Ecommerce.Application.Abstractions;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}