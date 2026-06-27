using GymManagement.Application.Common.Errors;
using GymManagement.Domain.Interfaces.Repositories;

namespace GymManagement.Application.Features.Offers.Commands.DeactivateOffer;

public record DeactivateOfferCommand(Guid Id);

public class DeactivateOfferCommandHandler(IOfferRepository offerRepo)
{
    public async Task<Result> Handle(DeactivateOfferCommand command, CancellationToken ct = default)
    {
        var offer = await offerRepo.GetByIdAsync(command.Id, ct);
        if (offer is null)
            return AppError.NotFound("Offer");

        offer.Deactivate();
        offerRepo.Update(offer);
        await offerRepo.SaveChangesAsync(ct);
        return Result.Success();
    }
}
