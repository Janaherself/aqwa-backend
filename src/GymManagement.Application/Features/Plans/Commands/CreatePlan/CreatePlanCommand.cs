namespace GymManagement.Application.Features.Plans.Commands.CreatePlan;

public record CreatePlanCommand(string Name, int DurationMonths, decimal BasePrice);
public record CreatePlanResult(Guid Id, string Name, int DurationMonths, decimal BasePrice);
