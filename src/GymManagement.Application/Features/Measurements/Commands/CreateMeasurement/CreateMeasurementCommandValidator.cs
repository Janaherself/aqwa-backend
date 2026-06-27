using FluentValidation;

namespace GymManagement.Application.Features.Measurements.Commands.CreateMeasurement;

public class CreateMeasurementCommandValidator : AbstractValidator<CreateMeasurementCommand>
{
    public CreateMeasurementCommandValidator()
    {
        RuleFor(x => x.MemberId).NotEmpty();

        var positiveDecimalFields = new (System.Linq.Expressions.Expression<Func<CreateMeasurementCommand, decimal?>> Expr, string Name)[]
        {
            (x => x.Weight, "Weight"), (x => x.Height, "Height"),
            (x => x.RightThigh, "RightThigh"), (x => x.LeftThigh, "LeftThigh"),
            (x => x.RightUpperArm, "RightUpperArm"), (x => x.LeftUpperArm, "LeftUpperArm"),
            (x => x.Hip, "Hip"), (x => x.Waist, "Waist"),
            (x => x.Chest, "Chest"), (x => x.Neck, "Neck"),
            (x => x.RightCalf, "RightCalf"), (x => x.LeftCalf, "LeftCalf"),
        };

        foreach (var (expr, name) in positiveDecimalFields)
            RuleFor(expr).GreaterThan(0).When(x => expr.Compile()(x).HasValue)
                         .WithName(name);

        RuleFor(x => x.BodyFatPercentage)
            .InclusiveBetween(1, 100).When(x => x.BodyFatPercentage.HasValue);

        RuleFor(x => x.Notes).MaximumLength(500);
    }
}
