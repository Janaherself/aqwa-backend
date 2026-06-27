using FluentValidation;
using GymManagement.Application.Features.Measurements.Commands.CreateMeasurement;
using GymManagement.Application.Features.Measurements.Queries.GetMeasurements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers;

[Authorize]
[Route("api/members/{memberId:guid}/measurements")]
public class MeasurementsController(
    CreateMeasurementCommandHandler createHandler,
    GetMeasurementsQueryHandler getHandler,
    IValidator<CreateMeasurementCommand> validator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(Guid memberId, CancellationToken ct)
    {
        var result = await getHandler.Handle(new GetMeasurementsQuery(memberId), ct);
        return FromResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid memberId, [FromBody] CreateMeasurementCommand command, CancellationToken ct)
    {
        var cmd = command with { MemberId = memberId };
        var validation = await validator.ValidateAsync(cmd, ct);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var result = await createHandler.Handle(cmd, ct);
        return result.IsSuccess ? Created(string.Empty, result.Value) : FromResult(result);
    }
}
