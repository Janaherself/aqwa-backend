using GymManagement.Application.Common.Errors;
using GymManagement.Domain.Interfaces.Repositories;

namespace GymManagement.Application.Features.Plans.Commands.UpdatePlan;

public class UpdatePlanCommandHandler(ISubscriptionPlanRepository planRepo)
{
    public async Task<Result> Handle(UpdatePlanCommand command, CancellationToken ct = default)
    {
        var plan = await planRepo.GetByIdAsync(command.Id, ct);
        if (plan is null)
            return AppError.NotFound("SubscriptionPlan");

        plan.Update(command.Name, command.DurationMonths, command.BasePrice);
        planRepo.Update(plan);
        await planRepo.SaveChangesAsync(ct);
        return Result.Success();
    }
}
