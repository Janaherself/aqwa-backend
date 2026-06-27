using GymManagement.Application.Common.Errors;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces.Repositories;
using GymManagement.Domain.Interfaces.Services;

namespace GymManagement.Application.Features.Staff.Commands.CreateStaff;

public class CreateStaffCommandHandler(
    IUserRepository userRepo,
    IRoleRepository roleRepo,
    IPasswordHasher passwordHasher,
    ICurrentGymContext gymContext)
{
    public async Task<Result<CreateStaffResult>> Handle(CreateStaffCommand command, CancellationToken ct = default)
    {
        if (await userRepo.UsernameExistsAsync(command.Username, ct))
            return AppError.Conflict($"Username '{command.Username}' is already taken.");

        var role = await roleRepo.GetByIdAsync(command.RoleId, ct);
        if (role is null)
            return AppError.NotFound("Role");

        var user = User.Create(gymContext.GymId, command.Username,
            passwordHasher.Hash(command.Password), command.PhoneNumber, command.RoleId);

        await userRepo.AddAsync(user, ct);
        await userRepo.SaveChangesAsync(ct);

        return new CreateStaffResult(user.Id, user.Username);
    }
}
