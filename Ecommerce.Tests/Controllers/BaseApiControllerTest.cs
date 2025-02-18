using System.ComponentModel.DataAnnotations;
using Ecommerce.Api.Rest.Controllers;
using Ecommerce.Services.Utilities.Exceptions;
using Ecommerce.Tests.TestExtensions;
using Ecommerce.Tests.Utilities.Samples;
using Ecommerce.Tests.Utilities.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Ecommerce.Tests.Controllers;

public class BaseApiControllerTest
{
    private readonly BaseApiControllerConcrete baseApiController = new();

    [Fact]
    public async Task ExecuteReadOrUpdateAsyncShouldReturnOk()
    {
        var response = ResponseDtoSamples.TestResponse;
        var result = await baseApiController.TestExecuteReadOrUpdateAsync(() => Task.FromResult(response));
        result.AssertSuccessResponse(response);
    }

    [Fact]
    public async Task ExecuteReadOrUpdateAsyncShouldFailWhenNotFound()
    {
        var result = await baseApiController.TestExecuteReadOrUpdateAsync<TestDto>(() => throw new RecordNotFoundException());
        result.AssertFailureResponse<TestDto>(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task ExecuteCreateAsyncShouldReturnOk()
    {
        var response = ResponseDtoSamples.TestResponse;
        var result = await baseApiController.TestExecuteCreateAsync(() => Task.FromResult(response));
        result.AssertSuccessResponse(response);
    }

    [Fact]
    public async Task ExecuteCreateAsyncShouldFailWhenInvalidInput()
    {
        var result = await baseApiController.TestExecuteCreateAsync<TestDto>(
            () => throw new ValidationAggregateException([new ValidationException("some invalid field")]));
        result.AssertFailureResponse<TestDto>(StatusCodes.Status400BadRequest);
    }
    
    [Fact]
    public async Task ExecuteCreateAsyncShouldFailWhenUnknownError()
    {
        var result = await baseApiController.TestExecuteCreateAsync<TestDto>(() => throw new Exception("Something went wrong"));
        result.AssertFailureResponse<TestDto>(StatusCodes.Status500InternalServerError);
    }

    private class BaseApiControllerConcrete : BaseApiController
    {
        public Task<IActionResult> TestExecuteReadOrUpdateAsync<T>(Func<Task<T>> func) where T : class
        {
            return ExecuteReadOrUpdateAsync(func);
        }

        public Task<IActionResult> TestExecuteCreateAsync<T>(Func<Task<T>> func) where T : class
        {
            return ExecuteCreateAsync(func);
        }
    }
}