namespace Domain.Models;

public class Result<T>
{
    private Result(bool isSuccess, T? value, string? error, int code)
    {
        this.isSuccess = isSuccess;
        this.value = value;
        this.error = error;
        this.code = code;
    }

    public bool isSuccess { get; set; }
    public T? value { get; set; }
    public string? error { get; set; }
    public int code { get; set; }

    public static Result<T> Success(T value, int code) => new(true, value, null, code);
    public static Result<T> Failure(string error, int code) => new(false, default, error, code);
    public static Result<T> NoContent() => new(true, default, null, 204);
}
