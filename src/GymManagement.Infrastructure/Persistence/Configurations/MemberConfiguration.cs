using GymManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Persistence.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.HasIndex(m => new { m.PhoneNumber, m.GymId }).IsUnique();
        builder.Property(m => m.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(m => m.LastName).IsRequired().HasMaxLength(50);
        builder.Property(m => m.Notes).HasMaxLength(500);

        builder.HasMany(m => m.Measurements)
            .WithOne()
            .HasForeignKey(ms => ms.MemberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.Subscriptions)
            .WithOne()
            .HasForeignKey(s => s.MemberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(m => m.FullName);
    }
}
