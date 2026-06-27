namespace GymManagement.Application.Features.Offers.Commands.CreateOffer;

public record CreateOfferCommand(
    string Name,
    Guid PlanId,
    int BonusDays,
    decimal? DiscountAmount,
    string? Description,
    DateTime? ValidFrom,
    DateTime? ValidTo);

public record CreateOfferResult(Guid Id, string Name);
