using FluentValidation;
using GymManagement.Application.Features.Staff.Commands.CreateStaff;
using GymManagement.Application.Features.Staff.Queries.GetStaff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers;

[Authorize]
public class StaffController(
    CreateStaffCommandHandler createHandler,
    GetStaffQueryHandler getHandler,
    IValidator<CreateStaffCommand> validator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await getHandler.Handle(new GetStaffQuery(), ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStaffCommand command, CancellationToken ct)
    {
        var validation = await validator.ValidateAsync(command, ct);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var result = await createHandler.Handle(command, ct);
        return result.IsSuccess ? Created(string.Empty, result.Value) : FromResult(result);
    }
}
