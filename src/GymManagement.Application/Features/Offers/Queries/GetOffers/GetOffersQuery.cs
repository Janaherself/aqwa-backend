namespace GymManagement.Application.Features.Offers.Queries.GetOffers;

public record GetOffersQuery(Guid? PlanId = null, bool ActiveOnly = false);

public record OfferDto(
    Guid Id,
    string Name,
    Guid PlanId,
    int BonusDays,
    decimal? DiscountAmount,
    string? Description,
    bool IsActive,
    DateTime? ValidFrom,
    DateTime? ValidTo);
