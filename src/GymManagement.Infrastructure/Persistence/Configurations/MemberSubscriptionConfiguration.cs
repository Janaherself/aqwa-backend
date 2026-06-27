using GymManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Persistence.Configurations;

public class MemberSubscriptionConfiguration : IEntityTypeConfiguration<MemberSubscription>
{
    public void Configure(EntityTypeBuilder<MemberSubscription> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.PriceCharged).HasPrecision(10, 2);
        builder.Property(s => s.Notes).HasMaxLength(500);
        builder.HasIndex(s => new { s.MemberId, s.ExpiresAt });
    }
}
