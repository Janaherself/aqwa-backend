using AutoMapper;
using GymManagement.Application.Features.Plans.Queries.GetPlans;
using GymManagement.Application.Features.Offers.Queries.GetOffers;
using GymManagement.Domain.Entities;

namespace GymManagement.Application.Mappings;

public class PlanMappingProfile : Profile
{
    public PlanMappingProfile()
    {
        CreateMap<SubscriptionPlan, PlanDto>();
        CreateMap<Offer, OfferDto>();
    }
}
