using FluentValidation;
using GymManagement.Application.Features.Offers.Commands.CreateOffer;
using GymManagement.Application.Features.Offers.Commands.DeactivateOffer;
using GymManagement.Application.Features.Offers.Queries.GetOffers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers;

[Authorize]
public class OffersController(
    CreateOfferCommandHandler createHandler,
    DeactivateOfferCommandHandler deactivateHandler,
    GetOffersQueryHandler getHandler,
    IValidator<CreateOfferCommand> validator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? planId, [FromQuery] bool activeOnly = false, CancellationToken ct = default)
    {
        var result = await getHandler.Handle(new GetOffersQuery(planId, activeOnly), ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOfferCommand command, CancellationToken ct)
    {
        var validation = await validator.ValidateAsync(command, ct);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var result = await createHandler.Handle(command, ct);
        return result.IsSuccess ? Created(string.Empty, result.Value) : FromResult(result);
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken ct)
    {
        var result = await deactivateHandler.Handle(new DeactivateOfferCommand(id), ct);
        return FromResult(result);
    }
}
