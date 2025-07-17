namespace Test.Models;

public class Response
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public object? Data { get; set; }
    public Response(bool success, string message, object? data = null)
    {
        Success = success;
        Message = message;
        Data = data;
    }
    public Response()
    {
        
    }
}
