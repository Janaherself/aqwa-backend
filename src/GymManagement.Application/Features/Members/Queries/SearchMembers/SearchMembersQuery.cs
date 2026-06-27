using GymManagement.Domain.Enums;

namespace GymManagement.Application.Features.Members.Queries.SearchMembers;

public record SearchMembersQuery(string? Name, string? Phone, bool? IsActive = null);

public record MemberSummaryDto(
    Guid Id,
    string PhoneNumber,
    string FullName,
    bool IsActive,
    Gender? Gender);
