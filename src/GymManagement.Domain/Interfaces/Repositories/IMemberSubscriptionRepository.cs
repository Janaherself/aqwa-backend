using GymManagement.Domain.Entities;

namespace GymManagement.Domain.Interfaces.Repositories;

public interface IMemberSubscriptionRepository : IRepository<MemberSubscription>
{
    Task<IEnumerable<MemberSubscription>> GetByMemberAsync(Guid memberId, CancellationToken ct = default);
    Task<MemberSubscription?> GetActiveByMemberAsync(Guid memberId, CancellationToken ct = default);
    Task<IEnumerable<(Member Member, MemberSubscription Subscription)>> GetExpiringSoonAsync(
        DateTime weekStart, DateTime weekEnd, CancellationToken ct = default);
}
