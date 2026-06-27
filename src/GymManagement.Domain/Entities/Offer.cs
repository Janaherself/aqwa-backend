using GymManagement.Domain.Common;

namespace GymManagement.Domain.Entities;

public class Offer : TenantEntity
{
    public string Name { get; private set; } = default!;
    public Guid PlanId { get; private set; }
    public int BonusDays { get; private set; }
    public decimal? DiscountAmount { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTime? ValidFrom { get; private set; }
    public DateTime? ValidTo { get; private set; }

    private Offer() { }

    public static Offer Create(Guid gymId, string name, Guid planId, int bonusDays,
        decimal? discountAmount = null, string? description = null,
        DateTime? validFrom = null, DateTime? validTo = null)
    {
        return new Offer
        {
            GymId = gymId,
            Name = name,
            PlanId = planId,
            BonusDays = bonusDays,
            DiscountAmount = discountAmount,
            Description = description,
            IsActive = true,
            ValidFrom = validFrom,
            ValidTo = validTo
        };
    }

    public void Update(string name, int bonusDays, decimal? discountAmount,
        string? description, DateTime? validFrom, DateTime? validTo)
    {
        Name = name;
        BonusDays = bonusDays;
        DiscountAmount = discountAmount;
        Description = description;
        ValidFrom = validFrom;
        ValidTo = validTo;
        SetUpdatedAt();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetUpdatedAt();
    }

    public bool IsCurrentlyActive(DateTime now) =>
        IsActive &&
        (!ValidFrom.HasValue || ValidFrom <= now) &&
        (!ValidTo.HasValue || ValidTo >= now);
}
