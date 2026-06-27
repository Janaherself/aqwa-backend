using GymManagement.Application.Common.Errors;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult FromResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error!.Code switch
        {
            var c when c.EndsWith(".NotFound") => NotFound(result.Error),
            "Conflict" => Conflict(result.Error),
            "Unauthorized" => Unauthorized(result.Error),
            "Validation" => BadRequest(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    protected IActionResult FromResult(Result result)
    {
        if (result.IsSuccess)
            return NoContent();

        return result.Error!.Code switch
        {
            var c when c.EndsWith(".NotFound") => NotFound(result.Error),
            "Conflict" => Conflict(result.Error),
            "Unauthorized" => Unauthorized(result.Error),
            "Validation" => BadRequest(result.Error),
            _ => BadRequest(result.Error)
        };
    }
}
