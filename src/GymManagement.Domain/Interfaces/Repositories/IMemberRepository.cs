using GymManagement.Domain.Entities;

namespace GymManagement.Domain.Interfaces.Repositories;

public interface IMemberRepository : IRepository<Member>
{
    Task<Member?> GetByPhoneAsync(string phoneNumber, CancellationToken ct = default);
    Task<bool> PhoneExistsAsync(string phoneNumber, CancellationToken ct = default);
    Task<IEnumerable<Member>> SearchByNameAsync(string name, CancellationToken ct = default);
    Task<Member?> GetWithSubscriptionsAsync(Guid memberId, CancellationToken ct = default);
    Task<Member?> GetWithMeasurementsAsync(Guid memberId, CancellationToken ct = default);
}
