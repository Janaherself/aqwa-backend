using FluentValidation;
using GymManagement.Application.Features.Subscriptions.Commands.CreateSubscription;
using GymManagement.Application.Features.Subscriptions.Queries.GetExpiringSoon;
using GymManagement.Application.Features.Subscriptions.Queries.GetMemberSubscriptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers;

[Authorize]
public class SubscriptionsController(
    CreateSubscriptionCommandHandler createHandler,
    GetMemberSubscriptionsQueryHandler getMemberSubsHandler,
    GetExpiringSoonQueryHandler expiringSoonHandler,
    IValidator<CreateSubscriptionCommand> validator) : BaseController
{
    [HttpGet("api/members/{memberId:guid}/subscriptions")]
    public async Task<IActionResult> GetByMember(Guid memberId, CancellationToken ct)
    {
        var result = await getMemberSubsHandler.Handle(new GetMemberSubscriptionsQuery(memberId), ct);
        return FromResult(result);
    }

    [HttpPost("api/members/{memberId:guid}/subscriptions")]
    public async Task<IActionResult> Create(Guid memberId, [FromBody] CreateSubscriptionCommand command, CancellationToken ct)
    {
        var cmd = command with { MemberId = memberId };
        var validation = await validator.ValidateAsync(cmd, ct);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var result = await createHandler.Handle(cmd, ct);
        return result.IsSuccess ? Created(string.Empty, result.Value) : FromResult(result);
    }

    [HttpGet("api/subscriptions/expiring-soon")]
    public async Task<IActionResult> GetExpiringSoon(CancellationToken ct)
    {
        var result = await expiringSoonHandler.Handle(new GetExpiringSoonQuery(), ct);
        return Ok(result);
    }
}
