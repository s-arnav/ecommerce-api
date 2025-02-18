using System.ComponentModel.DataAnnotations;
using Ecommerce.Services.Utilities.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Services.Utilities.Validations;

public class Validation
{
    private readonly List<Exception> exceptions = [];

    public static Validation Begin()
    {
        return new Validation();
    }

    public Validation Check()
    {
        if (!exceptions.IsNullOrEmpty())
        {
            throw new ValidationAggregateException(exceptions);
        }

        return this;
    }

    public Validation IsNotNull<T>(T? value, string fieldName) where T : class
    {
        if (value == null)
        {
            exceptions.Add(new ValidationException($"'{fieldName}' cannot be null"));
        }
        
        return this;
    }
    
    public Validation IsNotNullOrEmptyString(string value, string fieldName)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
        {
            exceptions.Add(new ValidationException($"'{fieldName}' cannot be null or an empty string"));
        }
        
        return this;
    }

    public Validation IsNotNullOrEmptyCollection<T>(IEnumerable<T> collection, string fieldName) where T : class
    {
        if (collection.IsNullOrEmpty())
        {
            exceptions.Add(new ValidationException($"'{fieldName}' cannot be null or an empty array"));
        }
        
        return this;
    }

    public Validation IsValidId(Guid value, string fieldName)
    {
        if (value == Guid.Empty || !Guid.TryParse(value.ToString(), out var _))
        {
            exceptions.Add(new ValidationException($"'{fieldName}' is not a valid id"));
        }

        return this;
    }
}