using System.Security.Claims;
using GymManagement.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace GymManagement.Infrastructure.Services;

public class CurrentGymContext(IHttpContextAccessor httpContextAccessor) : ICurrentGymContext
{
    public Guid GymId
    {
        get
        {
            var raw = httpContextAccessor.HttpContext?.User.FindFirstValue("gym_id");
            return raw is not null ? Guid.Parse(raw) : Guid.Empty;
        }
    }

    public Guid UserId
    {
        get
        {
            var raw = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return raw is not null ? Guid.Parse(raw) : Guid.Empty;
        }
    }
}
