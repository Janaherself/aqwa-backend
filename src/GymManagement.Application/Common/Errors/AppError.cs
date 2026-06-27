namespace GymManagement.Application.Common.Errors;

public record AppError(string Code, string Message)
{
    public static AppError NotFound(string resource) => new($"{resource}.NotFound", $"{resource} was not found.");
    public static AppError Conflict(string message) => new("Conflict", message);
    public static AppError Unauthorized() => new("Unauthorized", "You are not authorized to perform this action.");
    public static AppError Validation(string message) => new("Validation", message);
}
