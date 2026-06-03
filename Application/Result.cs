namespace Domain.Models;

public class Result<T>
{
    private Result(bool isSuccess, T value, int code)
    {
        this.isSuccess = isSuccess;
        this.value = value;
        this.code = code;
    }

    private Result(bool isSuccess, string error, int code)
    {
        this.isSuccess = isSuccess;
        this.error = error;
        this.code = code;
    }

    private Result(bool isSuccess, int code)
    {
        this.isSuccess = isSuccess;
        this.code = code;
    }

    public bool isSuccess { get; set; }
    public T value { get; set; }
    public string error { get; set; }
    public int code { get; set; }
    
    

    public static Result<T> Success(T value, int code)
    {
        return new Result<T>(true, value, code);
    }

    public static Result<T> Failure(string error, int code)
    {
        return new Result<T>(false, error, code);
    }
    
    public static Result<T> NoContent()
    {
        return new Result<T>(true, 204);
    }
}