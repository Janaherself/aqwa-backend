using GymManagement.Application.Common.Errors;
using GymManagement.Domain.Interfaces.Repositories;

namespace GymManagement.Application.Features.Members.Commands.DeactivateMember;

public record DeactivateMemberCommand(Guid Id);

public class DeactivateMemberCommandHandler(IMemberRepository memberRepo)
{
    public async Task<Result> Handle(DeactivateMemberCommand command, CancellationToken ct = default)
    {
        var member = await memberRepo.GetByIdAsync(command.Id, ct);
        if (member is null)
            return AppError.NotFound("Member");

        member.Deactivate();
        memberRepo.Update(member);
        await memberRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
