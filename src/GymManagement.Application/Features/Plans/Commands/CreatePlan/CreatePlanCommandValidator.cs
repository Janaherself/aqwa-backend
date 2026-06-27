using FluentValidation;

namespace GymManagement.Application.Features.Plans.Commands.CreatePlan;

public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
{
    public CreatePlanCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.DurationMonths).InclusiveBetween(1, 24);
        RuleFor(x => x.BasePrice).GreaterThan(0);
    }
}
