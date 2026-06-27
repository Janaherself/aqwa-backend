namespace GymManagement.Application.Features.Measurements.Queries.GetMeasurements;

public record GetMeasurementsQuery(Guid MemberId);

public record MeasurementDto(
    Guid Id,
    Guid MemberId,
    DateTime RecordedAt,
    decimal? Weight,
    decimal? Height,
    decimal? Bmi,
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
