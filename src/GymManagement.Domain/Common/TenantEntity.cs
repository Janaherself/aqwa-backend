namespace GymManagement.Domain.Common;

public abstract class TenantEntity : BaseEntity
{
    public Guid GymId { get; protected set; }
}
