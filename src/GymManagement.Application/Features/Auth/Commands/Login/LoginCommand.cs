namespace GymManagement.Application.Features.Auth.Commands.Login;

public record LoginCommand(string Username, string Password);

public record LoginResult(string Token, string Username, string Role);
