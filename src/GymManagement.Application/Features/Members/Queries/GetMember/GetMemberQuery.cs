using GymManagement.Domain.Enums;

namespace GymManagement.Application.Features.Members.Queries.GetMember;

public record GetMemberQuery(Guid Id);

public record MemberDto(
    Guid Id,
    string PhoneNumber,
    string FirstName,
    string LastName,
    string FullName,
    DateOnly? DateOfBirth,
    Gender? Gender,
    string? Notes,
    bool IsActive,
    DateTime CreatedAt);
