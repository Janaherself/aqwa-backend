using GymManagement.Application.Common.Errors;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces.Repositories;
using GymManagement.Domain.Interfaces.Services;

namespace GymManagement.Application.Features.Members.Commands.CreateMember;

public class CreateMemberCommandHandler(
    IMemberRepository memberRepo,
    ICurrentGymContext gymContext)
{
    public async Task<Result<CreateMemberResult>> Handle(CreateMemberCommand command, CancellationToken ct = default)
    {
        if (await memberRepo.PhoneExistsAsync(command.PhoneNumber, ct))
            return AppError.Conflict($"A member with phone number '{command.PhoneNumber}' already exists.");

        var member = Member.Create(
            gymContext.GymId,
            command.PhoneNumber,
            command.FirstName,
            command.LastName,
            command.DateOfBirth,
            command.Gender,
            command.Notes);

        await memberRepo.AddAsync(member, ct);
        await memberRepo.SaveChangesAsync(ct);

        return new CreateMemberResult(member.Id, member.FullName, member.PhoneNumber);
    }
}
