namespace GymManagement.Application.Features.Subscriptions.Queries.GetExpiringSoon;

public record GetExpiringSoonQuery;

public record ExpiringSoonDto(
    Guid MemberId,
    string MemberFullName,
    string PhoneNumber,
    Guid SubscriptionId,
    DateTime ExpiresAt,
    int DaysRemaining);
