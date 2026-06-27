using GymManagement.Domain.Entities;

namespace GymManagement.Domain.Interfaces.Repositories;

public interface IOfferRepository : IRepository<Offer>
{
    Task<IEnumerable<Offer>> GetActiveByPlanAsync(Guid planId, CancellationToken ct = default);
    Task<IEnumerable<Offer>> GetCurrentlyActiveAsync(CancellationToken ct = default);
}
