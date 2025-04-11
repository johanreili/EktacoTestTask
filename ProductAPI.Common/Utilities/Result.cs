namespace ProductAPI.Common.Utilities;

public class Result
{
    public bool IsSuccess { get; }
    public string Error { get; }
    public string ErrorCode { get; }
    public IDictionary<string, string[]> ValidationErrors { get; }

    protected Result(
        bool isSuccess,
        string error,
        string errorCode = null,
        IDictionary<string, string[]> validationErrors = null
    )
    {
        IsSuccess = isSuccess;
        Error = error;
        ErrorCode = errorCode;
        ValidationErrors = validationErrors;
    }

    public static Result Success() => new(true, null);

    public static Result Failure(string error, string errorCode = null) =>
        new(false, error, errorCode);

    public static Result ValidationFailure(IDictionary<string, string[]> validationErrors) =>
        new(false, "Validation failed", "VALIDATION_ERROR", validationErrors);

    public override string ToString() => IsSuccess ? "Success" : $"Failure: {Error}";
}

public class Result<T> : Result
{
    public T Value { get; }

    private Result(
        bool isSuccess,
        string error,
        T value,
        string errorCode = null,
        IDictionary<string, string[]> validationErrors = null
    )
        : base(isSuccess, error, errorCode, validationErrors)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(true, null, value);

    public static new Result<T> Failure(string error, string errorCode = null) =>
        new(false, error, default, errorCode);

    public static implicit operator Result<T>(T value) => Success(value);

    public override string ToString() => IsSuccess ? $"Success: {Value}" : $"Failure: {Error}";
}

public static class ResultExtensions
{
    public static Result NotFound(string error) => Result.Failure(error, "NOT_FOUND");

    // Add extension method to handle FluentValidation errors
    public static Result ValidationFailure(
        this FluentValidation.Results.ValidationResult validationResult
    )
    {
        var validationErrors = validationResult.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

        return Result.ValidationFailure(validationErrors);
    }
}
