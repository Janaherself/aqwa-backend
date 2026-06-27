using FluentValidation;
using GymManagement.Application.Features.Auth.Commands.Login;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers;

public class AuthController(
    LoginCommandHandler loginHandler,
    IValidator<LoginCommand> loginValidator) : BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken ct)
    {
        var validation = await loginValidator.ValidateAsync(command, ct);
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var result = await loginHandler.Handle(command, ct);
        return FromResult(result);
    }
}
