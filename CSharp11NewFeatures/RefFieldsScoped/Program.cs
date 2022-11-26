using RefFieldsScoped;

Example();

void Example()
{
    var textHolder = new TextHolder(16);
    Span<char> values = stackalloc char[7] { 'D', 'z', 'h', 'u', 'l', 'i', 'o' };
    textHolder.Append(values);

    Console.WriteLine(textHolder.ToString());
}

Span<int> CreateSpan(scoped ref int parameter)
    => Span<int>.Empty;

Span<int> CreateSpan2()
{
    scoped Span<int> span = stackalloc int[420];
    return Span<int>.Empty;
}