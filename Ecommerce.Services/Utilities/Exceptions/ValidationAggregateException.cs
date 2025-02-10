namespace Ecommerce.Services.Utilities.Exceptions;

public class ValidationAggregateException(IEnumerable<Exception> exceptions) 
    : AggregateException(exceptions)
{
}