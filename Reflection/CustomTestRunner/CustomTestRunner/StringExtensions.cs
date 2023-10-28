namespace CustomTestRunner;

public static class StringExtensions
{
    public static string Capitalize(this string input)
        => input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentNullException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => input.First().ToString().ToUpper() + input.Substring(1)
        };
}
