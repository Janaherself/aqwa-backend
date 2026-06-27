using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Persistence.Repositories;

public class MeasurementRepository(GymDbContext context) : BaseRepository<Measurement>(context), IMeasurementRepository
{
    public async Task<IEnumerable<Measurement>> GetByMemberAsync(Guid memberId, CancellationToken ct = default) =>
        await DbSet
            .Where(m => m.MemberId == memberId)
            .OrderByDescending(m => m.RecordedAt)
            .ToListAsync(ct);

    public async Task<Measurement?> GetLatestByMemberAsync(Guid memberId, CancellationToken ct = default) =>
        await DbSet
            .Where(m => m.MemberId == memberId)
            .OrderByDescending(m => m.RecordedAt)
            .FirstOrDefaultAsync(ct);
}
