using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CustomValidator;

public class ObjectValidator
{
    public ValidationResult Validate(object obj)
    {
        if (obj == null)
        {
            var result = new ValidationResult();

            result.AddError("Object", "Object is null.");

            return result;
        }

        var objectType = obj.GetType();

        var properties = objectType.GetProperties();

        var validationResult = new ValidationResult();

        foreach (var property in properties)
        {
            var propertyName = property.Name;
            var propertyValue = property.GetValue(obj);

            var attributes = property.GetCustomAttributes<ValidationAttribute>();

            foreach (var attribute in attributes)
            {
                var isValid = attribute.IsValid(propertyValue);

                if (!isValid)
                {
                    var errorMessage = attribute.FormatErrorMessage(propertyName);

                    validationResult.AddError(propertyName, errorMessage);
                }
            }
        }

        return validationResult;
    }
}
