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

        var validationResult = new ValidationResult();

        this.ValidateValidatable(obj, validationResult);
        this.ValidateProperties(obj, validationResult);

        return validationResult;
    }

    private void ValidateValidatable(object obj, ValidationResult validationResult)
    {
        if (obj is not IValidatable validatable)
        {
            return;
        }

        var additionalErrors = validatable.Validate();

        foreach (var additionalError in additionalErrors)
        {
            foreach (var additionalErrorsValue in additionalError.Value)
            {
                validationResult.AddError(additionalError.Key, additionalErrorsValue);
            }
        }
    }

    private void ValidateProperties(object obj, ValidationResult validationResult)
    {
        var objectType = obj.GetType();

        var properties = objectType.GetProperties();

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
    }
}
