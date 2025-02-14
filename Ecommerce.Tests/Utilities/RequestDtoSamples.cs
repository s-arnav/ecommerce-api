using Ecommerce.Services.RequestDtos;
using Tynamix.ObjectFiller;

namespace Ecommerce.Tests.Utilities;

public static class RequestDtoSamples
{
    public static CreateCategoryRequest CreateCategory => new Filler<CreateCategoryRequest>().Create();
    
    public static UpdateCategoryRequest UpdateCategory => new Filler<UpdateCategoryRequest>().Create();

}