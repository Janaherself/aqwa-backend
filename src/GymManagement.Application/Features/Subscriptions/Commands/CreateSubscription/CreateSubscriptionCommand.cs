namespace GymManagement.Application.Features.Subscriptions.Commands.CreateSubscription;

public record CreateSubscriptionCommand(
    Guid MemberId,
    Guid PlanId,
    Guid? OfferId,
    DateTime? StartedAt,
    string? Notes);

public record CreateSubscriptionResult(Guid Id, DateTime StartedAt, DateTime ExpiresAt, decimal PriceCharged);
