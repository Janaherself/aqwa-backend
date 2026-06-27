using GymManagement.Domain.Common;

namespace GymManagement.Domain.Entities;

public class SubscriptionPlan : TenantEntity
{
    public string Name { get; private set; } = default!;
    public int DurationMonths { get; private set; }
    public decimal BasePrice { get; private set; }
    public bool IsActive { get; private set; } = true;

    private SubscriptionPlan() { }

    public static SubscriptionPlan Create(Guid gymId, string name, int durationMonths, decimal basePrice)
    {
        return new SubscriptionPlan
        {
            GymId = gymId,
            Name = name,
            DurationMonths = durationMonths,
            BasePrice = basePrice
        };
    }

    public void Update(string name, int durationMonths, decimal basePrice)
    {
        Name = name;
        DurationMonths = durationMonths;
        BasePrice = basePrice;
        SetUpdatedAt();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetUpdatedAt();
    }
}
