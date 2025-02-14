using Ecommerce.API.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shouldly;

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
            response.Success.ShouldBeFalse();
            response.Data.ShouldBeNull();
            actionResult.GetResponseStatusCode().ShouldBe(expectedStatusCode);
            response.Errors.ShouldNotBeEmpty();
        });
    }

    public static void AssertSuccessResponse<T>(this IActionResult actionResult, T expectedResponse) where T : class
    {
        var response = actionResult.ToApiResponse<T>();
        Assert.Multiple(() =>
        {
            response.Success.ShouldBeTrue();
            response.Data.ShouldBe(expectedResponse);
            actionResult.GetResponseStatusCode().ShouldBeOneOf(StatusCodes.Status200OK, StatusCodes.Status201Created);
            response.Errors.ShouldBeEmpty();
        });
    }
}