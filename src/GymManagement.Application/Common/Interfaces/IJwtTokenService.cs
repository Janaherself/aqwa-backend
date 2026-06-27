using GymManagement.Domain.Entities;

namespace GymManagement.Application.Common.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user, Role role);
}
