using AutoMapper;
using GymManagement.Application.Features.Members.Queries.GetMember;
using GymManagement.Application.Features.Members.Queries.SearchMembers;
using GymManagement.Domain.Entities;

namespace GymManagement.Application.Mappings;

public class MemberMappingProfile : Profile
{
    public MemberMappingProfile()
    {
        CreateMap<Member, MemberDto>()
            .ForMember(d => d.FullName, o => o.MapFrom(s => s.FullName));

        CreateMap<Member, MemberSummaryDto>()
            .ForMember(d => d.FullName, o => o.MapFrom(s => s.FullName));
    }
}
