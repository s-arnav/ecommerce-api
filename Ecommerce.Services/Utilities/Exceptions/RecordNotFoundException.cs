namespace Ecommerce.Services.Utilities.Exceptions;

public class RecordNotFoundException(string message) 
    : Exception(message)
{
}