var (result1, result2) = (0L, 0L);

foreach (var line in File.ReadLines("Input 07.txt"))
{
    var data = line.Split([':', ' '], StringSplitOptions.RemoveEmptyEntries);

    var expectedResult = long.Parse(data[0]);
    var arguments = data[1..].Select(long.Parse).ToList();

    var firstTryResult = CheckEquation(arguments, expectedResult, Add, Multiply);

    result1 += firstTryResult ?? 0L;
    result2 += firstTryResult ?? CheckEquation(arguments, expectedResult, Add, Multiply, Concatenate) ?? 0L;
}

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

static long? CheckEquation(List<long> arguments, long expectedResult, params IEnumerable<Func<long, long, long>> operations)
{
    var stack = new Stack<(int Index, long Accumulator)>([(Index: 1, Accumulator: arguments[0])]);

    while (stack.TryPop(out var item))
    {
        if (item.Index < arguments.Count)
        {
            foreach (var result in operations.Select(o => o(item.Accumulator, arguments[item.Index])).Where(r => r <= expectedResult))
            {
                stack.Push((Index: item.Index + 1, Accumulator: result));
            }
        }
        else if (item.Accumulator == expectedResult)
        {
            return expectedResult;
        }
    }

    return null;
}

static long Add(long a, long b) => a + b;

static long Multiply(long a, long b) => a * b;

static long Concatenate(long a, long b) => a * (long)Math.Pow(10.0, Math.Floor(Math.Log10(b) + 1.0)) + b;
