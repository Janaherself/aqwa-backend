using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Persistence;

public class GymDbContext : DbContext
{
    private readonly ICurrentGymContext? _gymContext;

    public GymDbContext(DbContextOptions<GymDbContext> options, ICurrentGymContext? gymContext = null)
        : base(options)
    {
        _gymContext = gymContext;
    }

    public DbSet<Gym> Gyms => Set<Gym>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Measurement> Measurements => Set<Measurement>();
    public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
    public DbSet<Offer> Offers => Set<Offer>();
    public DbSet<MemberSubscription> MemberSubscriptions => Set<MemberSubscription>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    // EF Core evaluates this property at query-execution time (not model-build time)
    // because it is a member access on the DbContext instance.
    // Guid.Empty means "no active gym" (seeding / background context) → filter is bypassed.
    private Guid ActiveGymId => _gymContext?.GymId ?? Guid.Empty;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);

        modelBuilder.Entity<Member>().HasQueryFilter(m => ActiveGymId == Guid.Empty || m.GymId == ActiveGymId);
        modelBuilder.Entity<SubscriptionPlan>().HasQueryFilter(p => ActiveGymId == Guid.Empty || p.GymId == ActiveGymId);
        modelBuilder.Entity<Offer>().HasQueryFilter(o => ActiveGymId == Guid.Empty || o.GymId == ActiveGymId);
        modelBuilder.Entity<User>().HasQueryFilter(u => ActiveGymId == Guid.Empty || u.GymId == ActiveGymId);
        modelBuilder.Entity<Role>().HasQueryFilter(r => ActiveGymId == Guid.Empty || r.GymId == ActiveGymId);
    }
}
