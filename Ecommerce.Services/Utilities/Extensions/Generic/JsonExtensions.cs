using System.Text.Json;

namespace Ecommerce.Services.Utilities.Extensions.Generic;

public static class JsonExtensions
{
    private static readonly JsonSerializerOptions options = new() { WriteIndented = true };
        
    public static string ToJson(this object obj)
    {
        return JsonSerializer.Serialize(obj, options);
    }
}