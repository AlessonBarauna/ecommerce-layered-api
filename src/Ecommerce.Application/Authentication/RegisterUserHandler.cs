using Ecommerce.Application.Abstractions;
using Ecommerce.Application.Authentication;
using Ecommerce.Domain.Users;

public sealed class RegisterUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserHandler(
        IUserRepository  userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<RegisterUserResponse?> HandleAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(
            request.Email,
            cancellationToken
        );

        if (existingUser is not null)
        {
            return null;
        }

        var passwordHash = _passwordHasher.Hash(request.Password);

        var user = new User(
            Guid.NewGuid(),
            request.FullName,
            request.Email,
            passwordHash,
            UserRole.Customer);

        await _userRepository.AddAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new RegisterUserResponse(
            user.Id,
            user.FullName,
            user.Email,
            user.Role,
            user.CreatedAt);
    }
}