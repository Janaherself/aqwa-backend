using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Persistence.Repositories;

public class UserRepository(GymDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default) =>
        await DbSet.FirstOrDefaultAsync(u => u.Username == username, ct);

    public async Task<bool> UsernameExistsAsync(string username, CancellationToken ct = default) =>
        await DbSet.AnyAsync(u => u.Username == username, ct);

    public async Task<User?> GetWithRoleAsync(Guid userId, CancellationToken ct = default) =>
        await DbSet.FirstOrDefaultAsync(u => u.Id == userId, ct);
}
