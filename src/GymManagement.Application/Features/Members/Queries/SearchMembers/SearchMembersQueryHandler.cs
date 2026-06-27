using AutoMapper;
using GymManagement.Domain.Interfaces.Repositories;

namespace GymManagement.Application.Features.Members.Queries.SearchMembers;

public class SearchMembersQueryHandler(IMemberRepository memberRepo, IMapper mapper)
{
    public async Task<IEnumerable<MemberSummaryDto>> Handle(SearchMembersQuery query, CancellationToken ct = default)
    {
        IEnumerable<Domain.Entities.Member> members;

        if (!string.IsNullOrWhiteSpace(query.Phone))
        {
            var byPhone = await memberRepo.GetByPhoneAsync(query.Phone, ct);
            members = byPhone is null ? [] : [byPhone];
        }
        else if (!string.IsNullOrWhiteSpace(query.Name))
        {
            members = await memberRepo.SearchByNameAsync(query.Name, ct);
        }
        else
        {
            members = await memberRepo.GetAllAsync(ct);
        }

        if (query.IsActive.HasValue)
            members = members.Where(m => m.IsActive == query.IsActive.Value);

        return mapper.Map<IEnumerable<MemberSummaryDto>>(members);
    }
}
