using System.Runtime.CompilerServices;
using Ecommerce.Services.Utilities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Ecommerce.API.Utilities;

[ApiController]
[Produces("application/json")]
public abstract class BaseApiController : ControllerBase
{
    protected async Task<IActionResult> ExecuteReadOrUpdateAsync<TResponse>(Func<Task<TResponse>> func, [CallerMemberName] string caller = "") where TResponse : class
    {
        try
        {
            var data = await func();

            return Ok(new ApiResponse<TResponse>(true, "Success", data));
        }
        catch (ValidationAggregateException e)
        {
            Log.Error(e, "Validation Error: {message}", e.Message);
            Console.WriteLine("AggregateException: {0}", e.Message);
            return BadRequest(new ApiResponse<TResponse>(false, e.Message, null,
                e.InnerExceptions.Select(inner => inner.Message)));
        }
        catch (RecordNotFoundException e)
        {
            Log.Error(e, "Not Found: {message}", e.Message);
            return NotFound(new ApiResponse<TResponse>(false, e.Message, null, [e.Message]));
        }
        catch (Exception e)
        {
            Log.Error(e, "Unexpected error encountered in {caller}", caller);
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new ApiResponse<TResponse>(false, $"{e.GetType()}: {e.Message}", null, [e.Message]));
        }
    }
    
    protected async Task<IActionResult> ExecuteCreateAsync<TResponse>(Func<Task<TResponse>> func, [CallerMemberName] string caller = "") where TResponse : class
    {
        try
        {
            var data = await func();

            return CreatedAtAction(caller, new ApiResponse<TResponse>(true, "Success", data, []));
        }
        catch (ValidationAggregateException e)
        {
            Log.Error(e, "Validation Error: {message}", e.Message);
            return BadRequest(new ApiResponse<TResponse>(false, e.Message, null, e.InnerExceptions.Select(inner => inner.Message)));
        }
        catch (Exception e)
        {
            Log.Error(e, "Unexpected error encountered in {caller}", caller);
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new ApiResponse<TResponse>(false, $"{e.GetType()}: {e.Message}", null, [e.Message]));
        }
    }
}