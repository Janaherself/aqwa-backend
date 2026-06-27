using GymManagement.Domain.Common;

namespace GymManagement.Domain.Entities;

public class User : TenantEntity
{
    public string Username { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public string PhoneNumber { get; private set; } = default!;
    public Guid RoleId { get; private set; }
    public bool IsActive { get; private set; } = true;

    private User() { }

    public static User Create(Guid gymId, string username, string passwordHash, string phoneNumber, Guid roleId)
    {
        return new User
        {
            GymId = gymId,
            Username = username,
            PasswordHash = passwordHash,
            PhoneNumber = phoneNumber,
            RoleId = roleId
        };
    }

    public void UpdatePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
        SetUpdatedAt();
    }

    public void Update(string username, string phoneNumber, Guid roleId)
    {
        Username = username;
        PhoneNumber = phoneNumber;
        RoleId = roleId;
        SetUpdatedAt();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetUpdatedAt();
    }
}
