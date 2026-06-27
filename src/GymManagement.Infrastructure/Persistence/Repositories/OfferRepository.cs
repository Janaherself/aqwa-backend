using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Persistence.Repositories;

public class OfferRepository(GymDbContext context) : BaseRepository<Offer>(context), IOfferRepository
{
    public async Task<IEnumerable<Offer>> GetActiveByPlanAsync(Guid planId, CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        return await DbSet
            .Where(o => o.PlanId == planId && o.IsActive
                && (o.ValidFrom == null || o.ValidFrom <= now)
                && (o.ValidTo == null || o.ValidTo >= now))
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Offer>> GetCurrentlyActiveAsync(CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        return await DbSet
            .Where(o => o.IsActive
                && (o.ValidFrom == null || o.ValidFrom <= now)
                && (o.ValidTo == null || o.ValidTo >= now))
            .ToListAsync(ct);
    }
}
