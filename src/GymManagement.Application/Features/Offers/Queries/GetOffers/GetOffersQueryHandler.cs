using AutoMapper;
using GymManagement.Domain.Interfaces.Repositories;

namespace GymManagement.Application.Features.Offers.Queries.GetOffers;

public class GetOffersQueryHandler(IOfferRepository offerRepo, IMapper mapper)
{
    public async Task<IEnumerable<OfferDto>> Handle(GetOffersQuery query, CancellationToken ct = default)
    {
        IEnumerable<Domain.Entities.Offer> offers;

        if (query.PlanId.HasValue)
            offers = await offerRepo.GetActiveByPlanAsync(query.PlanId.Value, ct);
        else if (query.ActiveOnly)
            offers = await offerRepo.GetCurrentlyActiveAsync(ct);
        else
            offers = await offerRepo.GetAllAsync(ct);

        return mapper.Map<IEnumerable<OfferDto>>(offers);
    }
}
