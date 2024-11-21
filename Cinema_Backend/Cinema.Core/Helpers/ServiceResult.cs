using System.Net;

namespace Cinema.Core.Helpers;

public class ServiceResult
{
    public ServiceResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
    
    public ServiceResult(bool isSuccess, string? message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public int MovieId { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}
