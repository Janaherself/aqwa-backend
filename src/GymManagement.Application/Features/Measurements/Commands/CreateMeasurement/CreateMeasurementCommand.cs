namespace GymManagement.Application.Features.Measurements.Commands.CreateMeasurement;

public record CreateMeasurementCommand(
    Guid MemberId,
    DateTime? RecordedAt,
    decimal? Weight,
    decimal? Height,
    decimal? RightThigh,
    decimal? LeftThigh,
    decimal? RightUpperArm,
    decimal? LeftUpperArm,
    decimal? Hip,
    decimal? Waist,
    decimal? Chest,
    decimal? Neck,
    decimal? RightCalf,
    decimal? LeftCalf,
    decimal? BodyFatPercentage,
    string? Notes);

public record CreateMeasurementResult(Guid Id, DateTime RecordedAt);
