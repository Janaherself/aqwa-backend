using GymManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Persistence.Configurations;

public class MeasurementConfiguration : IEntityTypeConfiguration<Measurement>
{
    public void Configure(EntityTypeBuilder<Measurement> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Weight).HasPrecision(6, 2);
        builder.Property(m => m.Height).HasPrecision(5, 2);
        builder.Property(m => m.RightThigh).HasPrecision(5, 2);
        builder.Property(m => m.LeftThigh).HasPrecision(5, 2);
        builder.Property(m => m.RightUpperArm).HasPrecision(5, 2);
        builder.Property(m => m.LeftUpperArm).HasPrecision(5, 2);
        builder.Property(m => m.Hip).HasPrecision(5, 2);
        builder.Property(m => m.Waist).HasPrecision(5, 2);
        builder.Property(m => m.Chest).HasPrecision(5, 2);
        builder.Property(m => m.Neck).HasPrecision(5, 2);
        builder.Property(m => m.RightCalf).HasPrecision(5, 2);
        builder.Property(m => m.LeftCalf).HasPrecision(5, 2);
        builder.Property(m => m.BodyFatPercentage).HasPrecision(4, 1);
        builder.Property(m => m.Notes).HasMaxLength(500);
        builder.Ignore(m => m.Bmi);
    }
}
