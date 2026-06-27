using GymManagement.Application.Common.Errors;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Interfaces.Repositories;

namespace GymManagement.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler(
    IUserRepository userRepo,
    IRoleRepository roleRepo,
    IPasswordHasher passwordHasher,
    IJwtTokenService jwtService)
{
    public async Task<Result<LoginResult>> Handle(LoginCommand command, CancellationToken ct = default)
    {
        var user = await userRepo.GetByUsernameAsync(command.Username, ct);
        if (user is null || !user.IsActive)
            return AppError.Unauthorized();

        if (!passwordHasher.Verify(command.Password, user.PasswordHash))
            return AppError.Unauthorized();

        var role = await roleRepo.GetByIdAsync(user.RoleId, ct);
        if (role is null)
            return AppError.NotFound("Role");

        var token = jwtService.GenerateToken(user, role);
        return new LoginResult(token, user.Username, role.Name);
    }
}
