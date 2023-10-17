namespace CustomValidator;

public class ValidationResult
{
    public ValidationResult(bool isValid)
    {
        this.IsValid = isValid;

        this.Errors = new Dictionary<string, List<string>>();
    }

    public bool IsValid { get; set; }

    public IDictionary<string, List<string>> Errors { get; set; }
}
