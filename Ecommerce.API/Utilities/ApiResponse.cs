namespace Ecommerce.API.Utilities;

public class ApiResponse<T>(bool success, string message, T? data, IEnumerable<string>? errors = null) where T : class
{
    public bool Success { get; set; } = success;
    public string Message { get; set; } = message;
    public T? Data { get; set; } = data;
    public IEnumerable<string> Errors { get; set; } = errors ?? [];
}