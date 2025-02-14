using Ecommerce.Services.RequestDtos;
using Ecommerce.Tests.Utilities.Types;
using Tynamix.ObjectFiller;

namespace Ecommerce.Tests.Utilities.Samples;

public static class RequestDtoSamples
{
    public static TestDto TestRequest => new Filler<TestDto>().Create();
    public static CreateCategoryRequest CreateCategory => new Filler<CreateCategoryRequest>().Create();
    
    public static UpdateCategoryRequest UpdateCategory => new Filler<UpdateCategoryRequest>().Create();

}