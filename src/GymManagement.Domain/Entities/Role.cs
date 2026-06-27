using GymManagement.Domain.Common;
using GymManagement.Domain.Enums;

namespace GymManagement.Domain.Entities;

public class Role : TenantEntity
{
    public string Name { get; private set; } = default!;
    public Permission Permissions { get; private set; }

    private Role() { }

    public static Role Create(Guid gymId, string name, Permission permissions)
    {
        return new Role
        {
            GymId = gymId,
            Name = name,
            Permissions = permissions
        };
    }

    public void Update(string name, Permission permissions)
    {
        Name = name;
        Permissions = permissions;
        SetUpdatedAt();
    }

    public bool HasPermission(Permission permission) => (Permissions & permission) == permission;
}
