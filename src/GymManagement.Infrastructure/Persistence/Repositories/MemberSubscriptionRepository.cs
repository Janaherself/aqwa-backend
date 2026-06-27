using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Persistence.Repositories;

public class MemberSubscriptionRepository(GymDbContext context)
    : BaseRepository<MemberSubscription>(context), IMemberSubscriptionRepository
{
    public async Task<IEnumerable<MemberSubscription>> GetByMemberAsync(Guid memberId, CancellationToken ct = default) =>
        await DbSet
            .Where(s => s.MemberId == memberId)
            .OrderByDescending(s => s.StartedAt)
            .ToListAsync(ct);

    public async Task<MemberSubscription?> GetActiveByMemberAsync(Guid memberId, CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        return await DbSet
            .Where(s => s.MemberId == memberId && s.ExpiresAt >= now)
            .OrderByDescending(s => s.ExpiresAt)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<(Member Member, MemberSubscription Subscription)>> GetExpiringSoonAsync(
        DateTime weekStart, DateTime weekEnd, CancellationToken ct = default)
    {
        var results = await context.Members
            .Join(context.MemberSubscriptions,
                m => m.Id,
                s => s.MemberId,
                (m, s) => new { Member = m, Subscription = s })
            .Where(x => x.Subscription.ExpiresAt >= weekStart && x.Subscription.ExpiresAt <= weekEnd)
            .OrderBy(x => x.Subscription.ExpiresAt)
            .ToListAsync(ct);

        return results.Select(x => (x.Member, x.Subscription));
    }
}
