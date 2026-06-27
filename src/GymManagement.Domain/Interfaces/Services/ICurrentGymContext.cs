namespace GymManagement.Domain.Interfaces.Services;

public interface ICurrentGymContext
{
    Guid GymId { get; }
    Guid UserId { get; }
}
