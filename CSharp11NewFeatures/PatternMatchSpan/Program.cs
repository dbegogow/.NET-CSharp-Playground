ReadOnlySpan<char> text = "Dzhulio Begogov";

if (text is "Dzhulio Begogov")
{
    Console.WriteLine("Yes it was");
}

if (text is ['D', ..])
{
    Console.WriteLine("Name starts with D");
}