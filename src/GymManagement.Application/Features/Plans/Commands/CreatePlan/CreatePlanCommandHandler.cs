using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces.Repositories;
using GymManagement.Domain.Interfaces.Services;
using GymManagement.Application.Common.Errors;

namespace GymManagement.Application.Features.Plans.Commands.CreatePlan;

public class CreatePlanCommandHandler(ISubscriptionPlanRepository planRepo, ICurrentGymContext gymContext)
{
    public async Task<Result<CreatePlanResult>> Handle(CreatePlanCommand command, CancellationToken ct = default)
    {
        var plan = SubscriptionPlan.Create(gymContext.GymId, command.Name, command.DurationMonths, command.BasePrice);
        await planRepo.AddAsync(plan, ct);
        await planRepo.SaveChangesAsync(ct);
        return new CreatePlanResult(plan.Id, plan.Name, plan.DurationMonths, plan.BasePrice);
    }
}
