using GymManagement.Domain.Entities;

namespace GymManagement.Domain.Interfaces.Repositories;

public interface IMeasurementRepository : IRepository<Measurement>
{
    Task<IEnumerable<Measurement>> GetByMemberAsync(Guid memberId, CancellationToken ct = default);
    Task<Measurement?> GetLatestByMemberAsync(Guid memberId, CancellationToken ct = default);
}
