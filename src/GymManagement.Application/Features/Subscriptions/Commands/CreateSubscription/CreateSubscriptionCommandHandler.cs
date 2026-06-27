using GymManagement.Application.Common.Errors;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces.Repositories;

namespace GymManagement.Application.Features.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler(
    IMemberRepository memberRepo,
    ISubscriptionPlanRepository planRepo,
    IOfferRepository offerRepo,
    IMemberSubscriptionRepository subscriptionRepo)
{
    public async Task<Result<CreateSubscriptionResult>> Handle(CreateSubscriptionCommand command, CancellationToken ct = default)
    {
        var member = await memberRepo.GetByIdAsync(command.MemberId, ct);
        if (member is null)
            return AppError.NotFound("Member");

        var plan = await planRepo.GetByIdAsync(command.PlanId, ct);
        if (plan is null || !plan.IsActive)
            return AppError.NotFound("SubscriptionPlan");

        int bonusDays = 0;
        decimal price = plan.BasePrice;

        if (command.OfferId.HasValue)
        {
            var offer = await offerRepo.GetByIdAsync(command.OfferId.Value, ct);
            if (offer is null || !offer.IsCurrentlyActive(DateTime.UtcNow))
                return AppError.NotFound("Offer");
            if (offer.PlanId != plan.Id)
                return AppError.Validation("Offer does not apply to the selected plan.");

            bonusDays = offer.BonusDays;
            if (offer.DiscountAmount.HasValue)
                price = Math.Max(0, price - offer.DiscountAmount.Value);
        }

        var subscription = MemberSubscription.Create(
            command.MemberId, command.PlanId, plan.DurationMonths,
            price, command.OfferId, bonusDays, command.StartedAt, command.Notes);

        await subscriptionRepo.AddAsync(subscription, ct);
        await subscriptionRepo.SaveChangesAsync(ct);

        return new CreateSubscriptionResult(subscription.Id, subscription.StartedAt, subscription.ExpiresAt, subscription.PriceCharged);
    }
}
