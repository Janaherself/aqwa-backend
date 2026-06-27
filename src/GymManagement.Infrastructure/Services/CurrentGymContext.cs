using System.Security.Claims;
using GymManagement.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace GymManagement.Infrastructure.Services;

public class CurrentGymContext(IHttpContextAccessor httpContextAccessor) : ICurrentGymContext
{
    public Guid GymId => Guid.Parse(
        httpContextAccessor.HttpContext?.User.FindFirstValue("gym_id")
        ?? throw new InvalidOperationException("GymId claim not found."));

    public Guid UserId => Guid.Parse(
        httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new InvalidOperationException("UserId claim not found."));
}
