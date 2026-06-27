using GymManagement.Domain.Common;

namespace GymManagement.Domain.Entities;

public class Gym : BaseEntity
{
    public string Name { get; private set; } = default!;
    public string? Address { get; private set; }
    public string? Phone { get; private set; }

    private Gym() { }

    public static Gym Create(string name, string? address = null, string? phone = null)
    {
        return new Gym
        {
            Name = name,
            Address = address,
            Phone = phone
        };
    }

    public void Update(string name, string? address, string? phone)
    {
        Name = name;
        Address = address;
        Phone = phone;
        SetUpdatedAt();
    }
}
