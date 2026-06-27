using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Persistence;

public class GymDbContext(DbContextOptions<GymDbContext> options, ICurrentGymContext? gymContext = null)
    : DbContext(options)
{
    public DbSet<Gym> Gyms => Set<Gym>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Measurement> Measurements => Set<Measurement>();
    public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
    public DbSet<Offer> Offers => Set<Offer>();
    public DbSet<MemberSubscription> MemberSubscriptions => Set<MemberSubscription>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);

        if (gymContext is not null)
        {
            var gymId = gymContext.GymId;
            modelBuilder.Entity<Member>().HasQueryFilter(m => m.GymId == gymId);
            modelBuilder.Entity<SubscriptionPlan>().HasQueryFilter(p => p.GymId == gymId);
            modelBuilder.Entity<Offer>().HasQueryFilter(o => o.GymId == gymId);
            modelBuilder.Entity<User>().HasQueryFilter(u => u.GymId == gymId);
            modelBuilder.Entity<Role>().HasQueryFilter(r => r.GymId == gymId);
        }
    }
}
