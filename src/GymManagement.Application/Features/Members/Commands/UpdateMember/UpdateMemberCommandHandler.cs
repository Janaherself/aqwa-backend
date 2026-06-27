using GymManagement.Application.Common.Errors;
using GymManagement.Domain.Interfaces.Repositories;

namespace GymManagement.Application.Features.Members.Commands.UpdateMember;

public class UpdateMemberCommandHandler(IMemberRepository memberRepo)
{
    public async Task<Result> Handle(UpdateMemberCommand command, CancellationToken ct = default)
    {
        var member = await memberRepo.GetByIdAsync(command.Id, ct);
        if (member is null)
            return AppError.NotFound("Member");

        var phoneConflict = await memberRepo.GetByPhoneAsync(command.PhoneNumber, ct);
        if (phoneConflict is not null && phoneConflict.Id != command.Id)
            return AppError.Conflict($"Phone number '{command.PhoneNumber}' is already used by another member.");

        member.Update(command.PhoneNumber, command.FirstName, command.LastName,
            command.DateOfBirth, command.Gender, command.Notes);

        memberRepo.Update(member);
        await memberRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
