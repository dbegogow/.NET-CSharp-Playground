using System.Numerics;

var numbers = new[] { 1, 2, 3, 4, 5, 0.55 };

var sum = AddAll(numbers);

Console.WriteLine(sum);

T AddAll<T>(T[] values) where T : INumber<T>
{
    T result = T.Zero;
    foreach (var value in values)
    {
        result += value;
    }

    return result;
}