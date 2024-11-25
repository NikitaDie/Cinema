namespace Cinema.Core.Helpers.UnifiedResponse;

public class Result<T> : Result
{
    public T? Data { get; }

    public Result(bool isSuccess, Error error, T? data) : base(isSuccess, error)
    {
        Data = data;
    }
}