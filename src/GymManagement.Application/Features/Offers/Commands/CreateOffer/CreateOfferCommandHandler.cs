using GymManagement.Application.Common.Errors;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces.Repositories;
using GymManagement.Domain.Interfaces.Services;

namespace GymManagement.Application.Features.Offers.Commands.CreateOffer;

public class CreateOfferCommandHandler(
    IOfferRepository offerRepo,
    ISubscriptionPlanRepository planRepo,
    ICurrentGymContext gymContext)
{
    public async Task<Result<CreateOfferResult>> Handle(CreateOfferCommand command, CancellationToken ct = default)
    {
        var plan = await planRepo.GetByIdAsync(command.PlanId, ct);
        if (plan is null)
            return AppError.NotFound("SubscriptionPlan");

        var offer = Offer.Create(gymContext.GymId, command.Name, command.PlanId, command.BonusDays,
            command.DiscountAmount, command.Description, command.ValidFrom, command.ValidTo);

        await offerRepo.AddAsync(offer, ct);
        await offerRepo.SaveChangesAsync(ct);

        return new CreateOfferResult(offer.Id, offer.Name);
    }
}
