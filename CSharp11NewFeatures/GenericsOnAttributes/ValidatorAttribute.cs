namespace GenericsOnAttributes;

internal class ValidatorAttribute<TValidator> : Attribute
    where TValidator : IValidator
{
    public Type ValidatorType { get; }

    public ValidatorAttribute()
    {
        ValidatorType = typeof(TValidator);
    }
}
