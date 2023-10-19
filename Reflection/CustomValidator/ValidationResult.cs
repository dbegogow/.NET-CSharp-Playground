namespace CustomValidator;

public class ValidationResult
{
    public ValidationResult()
        => this.Errors = new Dictionary<string, List<string>>();

    public bool IsValid => !this.Errors.Any();

    public IDictionary<string, List<string>> Errors { get; set; }

    public void AddError(string name, string message)
    {
        if (!this.Errors.ContainsKey(name))
        {
            this.Errors[name] = new List<string>();
        }

        this.Errors[name].Add(message);
    }
}
