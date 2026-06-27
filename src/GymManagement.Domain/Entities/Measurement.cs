using GymManagement.Domain.Common;

namespace GymManagement.Domain.Entities;

public class Measurement : BaseEntity
{
    public Guid MemberId { get; private set; }
    public DateTime RecordedAt { get; private set; }

    public decimal? Weight { get; private set; }
    public decimal? Height { get; private set; }
    public decimal? RightThigh { get; private set; }
    public decimal? LeftThigh { get; private set; }
    public decimal? RightUpperArm { get; private set; }
    public decimal? LeftUpperArm { get; private set; }
    public decimal? Hip { get; private set; }
    public decimal? Waist { get; private set; }
    public decimal? Chest { get; private set; }
    public decimal? Neck { get; private set; }
    public decimal? RightCalf { get; private set; }
    public decimal? LeftCalf { get; private set; }
    public decimal? BodyFatPercentage { get; private set; }
    public string? Notes { get; private set; }

    public decimal? Bmi => (Weight.HasValue && Height.HasValue && Height > 0)
        ? Math.Round(Weight.Value / (Height.Value / 100 * (Height.Value / 100)), 2)
        : null;

    private Measurement() { }

    public static Measurement Create(Guid memberId, DateTime? recordedAt = null,
        decimal? weight = null, decimal? height = null,
        decimal? rightThigh = null, decimal? leftThigh = null,
        decimal? rightUpperArm = null, decimal? leftUpperArm = null,
        decimal? hip = null, decimal? waist = null,
        decimal? chest = null, decimal? neck = null,
        decimal? rightCalf = null, decimal? leftCalf = null,
        decimal? bodyFatPercentage = null, string? notes = null)
    {
        return new Measurement
        {
            MemberId = memberId,
            RecordedAt = recordedAt ?? DateTime.UtcNow,
            Weight = weight,
            Height = height,
            RightThigh = rightThigh,
            LeftThigh = leftThigh,
            RightUpperArm = rightUpperArm,
            LeftUpperArm = leftUpperArm,
            Hip = hip,
            Waist = waist,
            Chest = chest,
            Neck = neck,
            RightCalf = rightCalf,
            LeftCalf = leftCalf,
            BodyFatPercentage = bodyFatPercentage,
            Notes = notes
        };
    }
}
