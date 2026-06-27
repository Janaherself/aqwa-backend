namespace GymManagement.Application.Common.Errors;

public class Result<T>
{
    public T? Value { get; }
    public AppError? Error { get; }
    public bool IsSuccess => Error is null;

    private Result(T value) => Value = value;
    private Result(AppError error) => Error = error;

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(AppError error) => new(error);

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(AppError error) => Failure(error);
}

public class Result
{
    public AppError? Error { get; }
    public bool IsSuccess => Error is null;

    private Result() { }
    private Result(AppError error) => Error = error;

    public static Result Success() => new();
    public static Result Failure(AppError error) => new(error);

    public static implicit operator Result(AppError error) => Failure(error);
}
