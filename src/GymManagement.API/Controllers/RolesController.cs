using GymManagement.Application.Features.Roles.Queries.GetRoles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers;

[Authorize]
public class RolesController(GetRolesQueryHandler getHandler) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await getHandler.Handle(new GetRolesQuery(), ct);
        return Ok(result);
    }
}
