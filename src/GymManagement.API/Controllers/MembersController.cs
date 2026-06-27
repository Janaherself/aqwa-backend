using FluentValidation;
using GymManagement.Application.Features.Members.Commands.CreateMember;
using GymManagement.Application.Features.Members.Commands.DeactivateMember;
using GymManagement.Application.Features.Members.Commands.UpdateMember;
using GymManagement.Application.Features.Members.Queries.GetMember;
using GymManagement.Application.Features.Members.Queries.SearchMembers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers;

[Authorize]
public class MembersController(
    CreateMemberCommandHandler createHandler,
    UpdateMemberCommandHandler updateHandler,
    DeactivateMemberCommandHandler deactivateHandler,
    GetMemberQueryHandler getHandler,
    SearchMembersQueryHandler searchHandler,
    IValidator<CreateMemberCommand> createValidator,
    IValidator<UpdateMemberCommand> updateValidator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] string? phone,
        [FromQuery] bool? isActive, CancellationToken ct)
    {
        var result = await searchHandler.Handle(new SearchMembersQuery(name, phone, isActive), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct)
    {
        var result = await getHandler.Handle(new GetMemberQuery(id), ct);
        return FromResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMemberCommand command, CancellationToken ct)
    {
        var validation = await createValidator.ValidateAsync(command, ct);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var result = await createHandler.Handle(command, ct);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { id = result.Value!.Id }, result.Value)
            : FromResult(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMemberCommand command, CancellationToken ct)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");

        var validation = await updateValidator.ValidateAsync(command, ct);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var result = await updateHandler.Handle(command, ct);
        return FromResult(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken ct)
    {
        var result = await deactivateHandler.Handle(new DeactivateMemberCommand(id), ct);
        return FromResult(result);
    }
}
