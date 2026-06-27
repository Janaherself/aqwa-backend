using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Persistence.Repositories;

public class SubscriptionPlanRepository(GymDbContext context) : BaseRepository<SubscriptionPlan>(context), ISubscriptionPlanRepository
{
    public async Task<IEnumerable<SubscriptionPlan>> GetActiveAsync(CancellationToken ct = default) =>
        await DbSet.Where(p => p.IsActive).OrderBy(p => p.DurationMonths).ToListAsync(ct);
}
