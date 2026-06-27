namespace GymManagement.Application.Features.Subscriptions.Queries.GetMemberSubscriptions;

public record GetMemberSubscriptionsQuery(Guid MemberId);

public record MemberSubscriptionDto(
    Guid Id,
    Guid PlanId,
    string PlanName,
    Guid? OfferId,
    string? OfferName,
    DateTime StartedAt,
    DateTime ExpiresAt,
    decimal PriceCharged,
    string? Notes,
    bool IsActive,
    int DaysRemaining);
