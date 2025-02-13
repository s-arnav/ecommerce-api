using Moq;

namespace Ecommerce.Tests.Controllers;

public abstract class BaseControllerTest : IDisposable
{
    protected MockRepository mockRepository = new(MockBehavior.Strict);
    
    protected BaseControllerTest()
    {
        // initialize common mocks here
    }

    public void Dispose()
    {
        mockRepository.VerifyAll();
    }
}