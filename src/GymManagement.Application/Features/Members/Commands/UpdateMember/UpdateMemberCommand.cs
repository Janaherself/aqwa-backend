using GymManagement.Domain.Enums;

namespace GymManagement.Application.Features.Members.Commands.UpdateMember;

public record UpdateMemberCommand(
    Guid Id,
    string PhoneNumber,
    string FirstName,
    string LastName,
    DateOnly? DateOfBirth,
    Gender? Gender,
    string? Notes);
