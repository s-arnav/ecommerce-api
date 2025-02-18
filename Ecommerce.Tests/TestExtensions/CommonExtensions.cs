using Shouldly;

namespace Ecommerce.Tests.TestExtensions;

public static class CommonExtensions
{
    public static bool Matches<T>(this T value, T target)
    {
        target.ShouldBeEquivalentTo(value);
        return true;
    }
}