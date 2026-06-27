using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Persistence.Repositories;

public class MemberRepository(GymDbContext context) : BaseRepository<Member>(context), IMemberRepository
{
    public async Task<Member?> GetByPhoneAsync(string phoneNumber, CancellationToken ct = default) =>
        await DbSet.FirstOrDefaultAsync(m => m.PhoneNumber == phoneNumber, ct);

    public async Task<bool> PhoneExistsAsync(string phoneNumber, CancellationToken ct = default) =>
        await DbSet.AnyAsync(m => m.PhoneNumber == phoneNumber, ct);

    public async Task<IEnumerable<Member>> SearchByNameAsync(string name, CancellationToken ct = default)
    {
        var lower = name.ToLower();
        return await DbSet
            .Where(m => (m.FirstName + " " + m.LastName).ToLower().Contains(lower)
                     || m.FirstName.ToLower().Contains(lower)
                     || m.LastName.ToLower().Contains(lower))
            .OrderBy(m => m.FirstName)
            .ToListAsync(ct);
    }

    public async Task<Member?> GetWithSubscriptionsAsync(Guid memberId, CancellationToken ct = default) =>
        await DbSet
            .Include(m => m.Subscriptions)
            .FirstOrDefaultAsync(m => m.Id == memberId, ct);

    public async Task<Member?> GetWithMeasurementsAsync(Guid memberId, CancellationToken ct = default) =>
        await DbSet
            .Include(m => m.Measurements)
            .FirstOrDefaultAsync(m => m.Id == memberId, ct);
}
