using GymManagement.Application.Common.Errors;
using GymManagement.Domain.Interfaces.Repositories;

namespace GymManagement.Application.Features.Subscriptions.Queries.GetMemberSubscriptions;

public class GetMemberSubscriptionsQueryHandler(
    IMemberRepository memberRepo,
    IMemberSubscriptionRepository subscriptionRepo,
    ISubscriptionPlanRepository planRepo,
    IOfferRepository offerRepo)
{
    public async Task<Result<IEnumerable<MemberSubscriptionDto>>> Handle(GetMemberSubscriptionsQuery query, CancellationToken ct = default)
    {
        var member = await memberRepo.GetByIdAsync(query.MemberId, ct);
        if (member is null)
            return AppError.NotFound("Member");

        var subscriptions = await subscriptionRepo.GetByMemberAsync(query.MemberId, ct);
        var plans = (await planRepo.GetAllAsync(ct)).ToDictionary(p => p.Id);
        var offers = (await offerRepo.GetAllAsync(ct)).ToDictionary(o => o.Id);

        var now = DateTime.UtcNow;
        var result = subscriptions.Select(s => new MemberSubscriptionDto(
            s.Id,
            s.PlanId,
            plans.TryGetValue(s.PlanId, out var plan) ? plan.Name : "Unknown",
            s.OfferId,
            s.OfferId.HasValue && offers.TryGetValue(s.OfferId.Value, out var offer) ? offer.Name : null,
            s.StartedAt,
            s.ExpiresAt,
            s.PriceCharged,
            s.Notes,
            s.IsActive(now),
            s.DaysRemaining(now)));

        return Result<IEnumerable<MemberSubscriptionDto>>.Success(result);
    }
}
