using Ecommerce.API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Tests.TestExtensions;

public static class ControllerResponseExtensions
{
    public static ApiResponse<T> ToApiResponse<T>(this IActionResult actionResult) where T : class
    {
        var response = actionResult as ObjectResult;

        return response?.Value as ApiResponse<T>;
    }
    
    public static int GetResponseStatusCode(this IActionResult actionResult)
    {
        var response = actionResult as ObjectResult;

        return response?.StatusCode ?? 0;
    }

    public static void AssertFailureResponse<T>(this IActionResult actionResult, int expectedStatusCode) where T : class
    {
        var response = actionResult.ToApiResponse<T>();
        Assert.Multiple(() =>
        {
            Assert.That(response.Success, Is.False);
            Assert.That(response.Data, Is.Null);
            Assert.That(actionResult.GetResponseStatusCode(), Is.EqualTo(expectedStatusCode));
            Assert.That(response.Errors, Is.Not.Empty);
        });
    }

    public static void AssertSuccessResponse<T>(this IActionResult actionResult, T expectedResponse) where T : class
    {
        var response = actionResult.ToApiResponse<T>();
        Assert.Multiple(() =>
        {
            Assert.That(response.Success, Is.True);
            Assert.That(response.Data, Is.EqualTo(expectedResponse));
            Assert.That(actionResult.GetResponseStatusCode(), Is.AnyOf(200, 201));
            Assert.That(response.Errors, Is.Empty);
        });
    }
}