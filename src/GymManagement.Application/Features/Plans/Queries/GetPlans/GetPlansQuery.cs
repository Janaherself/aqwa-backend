namespace GymManagement.Application.Features.Plans.Queries.GetPlans;

public record GetPlansQuery(bool ActiveOnly = true);
public record PlanDto(Guid Id, string Name, int DurationMonths, decimal BasePrice, bool IsActive);
