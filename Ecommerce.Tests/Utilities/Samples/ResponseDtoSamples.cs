using Ecommerce.Services.ResponseDtos;
using Ecommerce.Tests.Utilities.Types;
using Tynamix.ObjectFiller;

namespace Ecommerce.Tests.Utilities.Samples;

public static class ResponseDtoSamples
{
    public static TestDto TestResponse => new Filler<TestDto>().Create();
    public static List<CategoryResponse> Categories(int count = 3)
        => new Filler<CategoryResponse>().Create(count).ToList();
    
    public static CategoryResponse Category => new Filler<CategoryResponse>().Create();
}