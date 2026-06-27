using GymManagement.Domain.Interfaces.Repositories;

namespace GymManagement.Application.Features.Roles.Queries.GetRoles;

public record GetRolesQuery;
public record RoleDto(Guid Id, string Name, long Permissions);

public class GetRolesQueryHandler(IRoleRepository roleRepo)
{
    public async Task<IEnumerable<RoleDto>> Handle(GetRolesQuery query, CancellationToken ct = default)
    {
        var roles = await roleRepo.GetAllAsync(ct);
        return roles.Select(r => new RoleDto(r.Id, r.Name, (long)r.Permissions));
    }
}
