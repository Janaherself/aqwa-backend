using GymManagement.Domain.Common;
using GymManagement.Domain.Enums;

namespace GymManagement.Domain.Entities;

public class Member : TenantEntity
{
    public string PhoneNumber { get; private set; } = default!;
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public DateOnly? DateOfBirth { get; private set; }
    public Gender? Gender { get; private set; }
    public string? Notes { get; private set; }
    public bool IsActive { get; private set; } = true;

    public IReadOnlyCollection<Measurement> Measurements => _measurements.AsReadOnly();
    public IReadOnlyCollection<MemberSubscription> Subscriptions => _subscriptions.AsReadOnly();

    private readonly List<Measurement> _measurements = [];
    private readonly List<MemberSubscription> _subscriptions = [];

    private Member() { }

    public static Member Create(Guid gymId, string phoneNumber, string firstName, string lastName,
        DateOnly? dateOfBirth = null, Gender? gender = null, string? notes = null)
    {
        return new Member
        {
            GymId = gymId,
            PhoneNumber = phoneNumber,
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth,
            Gender = gender,
            Notes = notes
        };
    }

    public void Update(string phoneNumber, string firstName, string lastName,
        DateOnly? dateOfBirth, Gender? gender, string? notes)
    {
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        Notes = notes;
        SetUpdatedAt();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetUpdatedAt();
    }

    public void Reactivate()
    {
        IsActive = true;
        SetUpdatedAt();
    }

    public string FullName => $"{FirstName} {LastName}";
}
