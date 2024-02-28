namespace ChaseTheFlag.Client.Data.Authentication.Additions
{

    /// <summary>
    /// Represents the result of an operation.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    public class Result<T>
    {
        public ResultStatus Status { get; }           // The status of the result.
        public T Value { get; }                       // The value of the result.
        public string ErrorMessage { get; }           // The error message associated with the result.

        // Constructor
        private Result(ResultStatus status, T value, string errorMessage)
        {
            Status = status;
            Value = value;
            ErrorMessage = errorMessage;
        }

        // Factory method for creating a successful result
        public static Result<T> Success(T value, string errorMessage = "") => new(ResultStatus.Success, value, errorMessage!);

        // Factory method for creating a failed result
        public static Result<T> Fail(string errorMessage) => new(ResultStatus.Fail, default!, errorMessage);

        // Factory method for creating a result with a mistake
        public static Result<T> Mistake(string errorMessage) => new(ResultStatus.Mistake, default!, errorMessage);
    }
}
