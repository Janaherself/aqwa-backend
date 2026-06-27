using FluentValidation;

namespace GymManagement.Application.Features.Members.Commands.UpdateMember;

public class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
{
    public UpdateMemberCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().MaximumLength(20)
            .Matches(@"^\+?[\d\s\-()]{7,20}$").WithMessage("Invalid phone number format.");
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.DateOfBirth)
            .Must(d => d == null || d < DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Date of birth must be in the past.");
        RuleFor(x => x.Notes).MaximumLength(500);
    }
}
