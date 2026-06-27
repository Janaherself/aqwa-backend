using AutoMapper;
using GymManagement.Application.Features.Measurements.Queries.GetMeasurements;
using GymManagement.Domain.Entities;

namespace GymManagement.Application.Mappings;

public class MeasurementMappingProfile : Profile
{
    public MeasurementMappingProfile()
    {
        CreateMap<Measurement, MeasurementDto>()
            .ForMember(d => d.Bmi, o => o.MapFrom(s => s.Bmi));
    }
}
