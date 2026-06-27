using AutoMapper;
using GymManagement.Domain.Interfaces.Repositories;

namespace GymManagement.Application.Features.Plans.Queries.GetPlans;

public class GetPlansQueryHandler(ISubscriptionPlanRepository planRepo, IMapper mapper)
{
    public async Task<IEnumerable<PlanDto>> Handle(GetPlansQuery query, CancellationToken ct = default)
    {
        var plans = query.ActiveOnly
            ? await planRepo.GetActiveAsync(ct)
            : await planRepo.GetAllAsync(ct);
        return mapper.Map<IEnumerable<PlanDto>>(plans);
    }
}
