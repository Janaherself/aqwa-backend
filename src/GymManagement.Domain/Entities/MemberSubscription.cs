using GymManagement.Domain.Common;

namespace GymManagement.Domain.Entities;

public class MemberSubscription : BaseEntity
{
    public Guid MemberId { get; private set; }
    public Guid PlanId { get; private set; }
    public Guid? OfferId { get; private set; }
    public DateTime StartedAt { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public decimal PriceCharged { get; private set; }
    public string? Notes { get; private set; }

    public bool IsActive(DateTime now) => ExpiresAt >= now;
    public int DaysRemaining(DateTime now) => Math.Max(0, (ExpiresAt.Date - now.Date).Days);

    private MemberSubscription() { }

    public static MemberSubscription Create(
        Guid memberId, Guid planId, int durationMonths,
        decimal priceCharged, Guid? offerId = null,
        int bonusDays = 0, DateTime? startedAt = null, string? notes = null)
    {
        var start = startedAt ?? DateTime.UtcNow;
        return new MemberSubscription
        {
            MemberId = memberId,
            PlanId = planId,
            OfferId = offerId,
            StartedAt = start,
            ExpiresAt = start.AddMonths(durationMonths).AddDays(bonusDays),
            PriceCharged = priceCharged,
            Notes = notes
        };
    }
}
