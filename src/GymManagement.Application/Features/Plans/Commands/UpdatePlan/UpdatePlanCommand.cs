namespace GymManagement.Application.Features.Plans.Commands.UpdatePlan;

public record UpdatePlanCommand(Guid Id, string Name, int DurationMonths, decimal BasePrice);
