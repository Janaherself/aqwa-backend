namespace GymManagement.Application.Features.Staff.Commands.CreateStaff;

public record CreateStaffCommand(string Username, string Password, string PhoneNumber, Guid RoleId);
public record CreateStaffResult(Guid Id, string Username);
