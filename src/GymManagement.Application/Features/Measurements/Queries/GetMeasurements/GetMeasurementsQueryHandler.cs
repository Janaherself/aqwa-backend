using AutoMapper;
using GymManagement.Application.Common.Errors;
using GymManagement.Domain.Interfaces.Repositories;

namespace GymManagement.Application.Features.Measurements.Queries.GetMeasurements;

public class GetMeasurementsQueryHandler(IMeasurementRepository measurementRepo, IMemberRepository memberRepo, IMapper mapper)
{
    public async Task<Result<IEnumerable<MeasurementDto>>> Handle(GetMeasurementsQuery query, CancellationToken ct = default)
    {
        var member = await memberRepo.GetByIdAsync(query.MemberId, ct);
        if (member is null)
            return AppError.NotFound("Member");

        var measurements = await measurementRepo.GetByMemberAsync(query.MemberId, ct);
        return Result<IEnumerable<MeasurementDto>>.Success(mapper.Map<IEnumerable<MeasurementDto>>(measurements));
    }
}
