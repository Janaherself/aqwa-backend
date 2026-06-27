using GymManagement.Domain.Interfaces.Repositories;

namespace GymManagement.Application.Features.Subscriptions.Queries.GetExpiringSoon;

public class GetExpiringSoonQueryHandler(IMemberSubscriptionRepository subscriptionRepo)
{
    public async Task<IEnumerable<ExpiringSoonDto>> Handle(GetExpiringSoonQuery query, CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;

        // Week boundaries: Sunday = DayOfWeek 0, Saturday = DayOfWeek 6
        int daysToSunday = -(int)now.DayOfWeek;
        var weekStart = now.Date.AddDays(daysToSunday);
        var weekEnd = weekStart.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59);

        var expiring = await subscriptionRepo.GetExpiringSoonAsync(weekStart, weekEnd, ct);

        return expiring.Select(e => new ExpiringSoonDto(
            e.Member.Id,
            e.Member.FullName,
            e.Member.PhoneNumber,
            e.Subscription.Id,
            e.Subscription.ExpiresAt,
            e.Subscription.DaysRemaining(now)));
    }
}
