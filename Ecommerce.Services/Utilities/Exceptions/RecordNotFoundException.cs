namespace Ecommerce.Services.Utilities.Exceptions;

public class RecordNotFoundException(string message = "Record not found") 
    : Exception(message)
{
}