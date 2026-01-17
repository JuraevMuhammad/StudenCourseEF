using System.Net;
using System.Runtime.InteropServices.JavaScript;

namespace Domain.Responses;

public class Response<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public int StatusCode { get; set; }

    public Response(T? data)
    {
        Data = data;
        Message = "success";
        StatusCode = 200;
    }
    
    public Response(HttpStatusCode code, string message)
    {
        Data = default;
        Message = message;
        StatusCode = (int)code;
    }
}