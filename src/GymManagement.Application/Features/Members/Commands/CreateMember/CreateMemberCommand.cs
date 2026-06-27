using GymManagement.Domain.Enums;

namespace GymManagement.Application.Features.Members.Commands.CreateMember;

public record CreateMemberCommand(
    string PhoneNumber,
    string FirstName,
    string LastName,
    DateOnly? DateOfBirth,
    Gender? Gender,
    string? Notes);

public record CreateMemberResult(Guid Id, string FullName, string PhoneNumber);
