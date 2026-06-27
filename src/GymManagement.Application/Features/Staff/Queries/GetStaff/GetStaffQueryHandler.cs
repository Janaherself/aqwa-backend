using GymManagement.Domain.Interfaces.Repositories;

namespace GymManagement.Application.Features.Staff.Queries.GetStaff;

public class GetStaffQueryHandler(IUserRepository userRepo, IRoleRepository roleRepo)
{
    public async Task<IEnumerable<StaffDto>> Handle(GetStaffQuery query, CancellationToken ct = default)
    {
        var users = await userRepo.GetAllAsync(ct);
        var roles = (await roleRepo.GetAllAsync(ct)).ToDictionary(r => r.Id);

        return users.Select(u => new StaffDto(
            u.Id,
            u.Username,
            u.PhoneNumber,
            roles.TryGetValue(u.RoleId, out var role) ? role.Name : "Unknown",
            u.IsActive));
    }
}
