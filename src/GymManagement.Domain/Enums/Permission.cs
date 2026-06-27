namespace GymManagement.Domain.Enums;

[Flags]
public enum Permission
{
    None = 0,
    ViewMembers = 1 << 0,
    ManageMembers = 1 << 1,
    ViewMeasurements = 1 << 2,
    ManageMeasurements = 1 << 3,
    ViewSubscriptions = 1 << 4,
    ManageSubscriptions = 1 << 5,
    ManagePlans = 1 << 6,
    ManageOffers = 1 << 7,
    ManageStaff = 1 << 8,
    ManageRoles = 1 << 9,
    ManageGym = 1 << 10,

    Coach = ViewMembers | ManageMembers | ViewMeasurements | ManageMeasurements
          | ViewSubscriptions | ManageSubscriptions,

    Admin = Coach | ManagePlans | ManageOffers | ManageStaff | ManageRoles | ManageGym
}
