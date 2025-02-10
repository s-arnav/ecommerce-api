using System.Runtime.CompilerServices;
using Ecommerce.Services.Utilities.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Utilities;

[ApiController]
[Produces("application/json")]
public abstract class BaseApiController : ControllerBase
{
    protected async Task<IActionResult> ExecuteReadOrUpdateAsync<T>(Func<Task<T>> func) where T : class
    {
        try
        {
            var data = await func();

            return Ok(new ApiResponse<T>(true, "Success", data, []));
        }
        catch (ValidationAggregateException e)
        {
            Console.WriteLine("AggregateException: {0}", e.Message);
            return BadRequest(new ApiResponse<T>(false, e.Message, default, e.InnerExceptions.Select(inner => inner.Message)));
        }
        catch (RecordNotFoundException e)
        {
            Console.WriteLine("Error: {0}", e.Message);
            return NotFound(new ApiResponse<T>(false, e.Message, default, [e.Message]));
        }
    }
    
    protected async Task<IActionResult> ExecuteCreateAsync<T>(Func<Task<T>> func, [CallerMemberName] string caller = "") where T : class
    {
        try
        {
            var data = await func();

            return CreatedAtAction(caller, new ApiResponse<T>(true, "Success", data, []));
        }
        catch (AggregateException e)
        {
            Console.WriteLine("AggregateException: {0}", e.Message);
            return BadRequest(new ApiResponse<T>(false, e.Message, default, e.InnerExceptions.Select(inner => inner.Message)));
        }
    }
}