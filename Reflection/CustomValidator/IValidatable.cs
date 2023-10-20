namespace CustomValidator;

public interface IValidatable
{
    IDictionary<string, List<string>> Validate();
}
