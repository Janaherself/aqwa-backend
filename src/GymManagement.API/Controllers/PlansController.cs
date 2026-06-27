using FluentValidation;
using GymManagement.Application.Features.Plans.Commands.CreatePlan;
using GymManagement.Application.Features.Plans.Commands.UpdatePlan;
using GymManagement.Application.Features.Plans.Queries.GetPlans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers;

[Authorize]
public class PlansController(
    CreatePlanCommandHandler createHandler,
    UpdatePlanCommandHandler updateHandler,
    GetPlansQueryHandler getHandler,
    IValidator<CreatePlanCommand> validator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool activeOnly = true, CancellationToken ct = default)
    {
        var result = await getHandler.Handle(new GetPlansQuery(activeOnly), ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlanCommand command, CancellationToken ct)
    {
        var validation = await validator.ValidateAsync(command, ct);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var result = await createHandler.Handle(command, ct);
        return result.IsSuccess ? Created(string.Empty, result.Value) : FromResult(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePlanCommand command, CancellationToken ct)
    {
        var result = await updateHandler.Handle(command with { Id = id }, ct);
        return FromResult(result);
    }
}
