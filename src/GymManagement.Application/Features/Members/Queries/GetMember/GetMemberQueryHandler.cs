using AutoMapper;
using GymManagement.Application.Common.Errors;
using GymManagement.Domain.Interfaces.Repositories;

namespace GymManagement.Application.Features.Members.Queries.GetMember;

public class GetMemberQueryHandler(IMemberRepository memberRepo, IMapper mapper)
{
    public async Task<Result<MemberDto>> Handle(GetMemberQuery query, CancellationToken ct = default)
    {
        var member = await memberRepo.GetByIdAsync(query.Id, ct);
        if (member is null)
            return AppError.NotFound("Member");

        return mapper.Map<MemberDto>(member);
    }
}
