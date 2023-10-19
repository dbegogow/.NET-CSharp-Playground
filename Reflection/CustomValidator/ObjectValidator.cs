using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CustomValidator;

public class ObjectValidator
{
    public ValidationResult Validate(object obj)
    {
        if (obj == null)
        {
            var result = new ValidationResult(false);

            result.Errors.Add("Object", new List<string> { "Object is null." });

            return result;
        }

        var objectType = obj.GetType();

        var properties = objectType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);

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
                    var errorMessage = attribute.FormatErrorMessage();
                }
            }
        }
    }
}
