using GymManagement.Application.Common.Errors;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces.Repositories;

namespace GymManagement.Application.Features.Measurements.Commands.CreateMeasurement;

public class CreateMeasurementCommandHandler(
    IMeasurementRepository measurementRepo,
    IMemberRepository memberRepo)
{
    public async Task<Result<CreateMeasurementResult>> Handle(CreateMeasurementCommand command, CancellationToken ct = default)
    {
        var member = await memberRepo.GetByIdAsync(command.MemberId, ct);
        if (member is null)
            return AppError.NotFound("Member");

        var measurement = Measurement.Create(
            command.MemberId, command.RecordedAt,
            command.Weight, command.Height,
            command.RightThigh, command.LeftThigh,
            command.RightUpperArm, command.LeftUpperArm,
            command.Hip, command.Waist,
            command.Chest, command.Neck,
            command.RightCalf, command.LeftCalf,
            command.BodyFatPercentage, command.Notes);

        await measurementRepo.AddAsync(measurement, ct);
        await measurementRepo.SaveChangesAsync(ct);

        return new CreateMeasurementResult(measurement.Id, measurement.RecordedAt);
    }
}
