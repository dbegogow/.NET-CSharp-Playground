// Before
static void WriteNumbersCount(params int[] numbers)
   => Console.WriteLine(numbers.Length);

// .NET 9
static void WriteNumbersCount1(params ReadOnlySpan<int> numbers) =>
    Console.WriteLine(numbers.Length);

static void WriteNumbersCount2(params IEnumerable<int> numbers) =>
    Console.WriteLine(numbers.Count());

static void WriteNumbersCount3(params HashSet<int> numbers) =>
    Console.WriteLine(numbers.Count);