using Ecommerce.Services.ResponseDtos;
using Tynamix.ObjectFiller;

namespace Ecommerce.Tests.Utilities;

public static class ResponseDtoSamples
{
    public static List<CategoryResponse> Categories(int count = 3)
        => new Filler<CategoryResponse>().Create(count).ToList();
    
    public static CategoryResponse Category => new Filler<CategoryResponse>().Create();
}