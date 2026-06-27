namespace GymManagement.Application.Features.Staff.Queries.GetStaff;

public record GetStaffQuery;
public record StaffDto(Guid Id, string Username, string PhoneNumber, string RoleName, bool IsActive);
