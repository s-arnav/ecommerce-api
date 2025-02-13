using System.ComponentModel.DataAnnotations;
using Ecommerce.API.Utilities;
using Ecommerce.Services.Utilities.Exceptions;
using Ecommerce.Tests.TestExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Tests.Controllers;

public class BaseApiControllerTest
{
    private readonly BaseApiControllerConcrete baseApiController = new();

    [Test]
    public async Task ExecuteReadOrUpdateAsyncShouldReturnOk()
    {
        var param = new { };
        var result = await baseApiController.TestExecuteReadOrUpdateAsync(() => Task.FromResult(param));
        Assert.That(result.GetResponseStatusCode(), Is.EqualTo(StatusCodes.Status200OK));
    }

    [Test]
    public async Task ExecuteReadOrUpdateAsyncShouldFailWhenNotFound()
    {
        var param = new { };
        var result = await baseApiController.TestExecuteReadOrUpdateAsync(() =>
            Task.FromException<RecordNotFoundException>(new RecordNotFoundException()));
        Assert.That(result.GetResponseStatusCode(), Is.EqualTo(StatusCodes.Status404NotFound));
    }

    [Test]
    public async Task ExecuteCreateAsyncShouldReturnOk()
    {
        var param = new { };
        var result = await baseApiController.TestExecuteCreateAsync(() => Task.FromResult(param));
        Assert.That(result.GetResponseStatusCode(), Is.EqualTo(StatusCodes.Status201Created));
    }

    [Test]
    public async Task ExecuteCreateAsyncShouldFailWhenInvalidInput()
    {
        var result = await baseApiController.TestExecuteCreateAsync(() =>
            Task.FromException<ValidationAggregateException>(new ValidationAggregateException([new ValidationException("some invalid field")])));
        Assert.That(result.GetResponseStatusCode(), Is.EqualTo(StatusCodes.Status400BadRequest));
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