var (result1, result2) = (0L, 0L);

await foreach (var line in File.ReadLinesAsync("Input 07.txt"))
{
    var data = line.Split([':', ' '], StringSplitOptions.RemoveEmptyEntries);

    var expectedResult = long.Parse(data[0]);
    var arguments = data.Skip(1).Select(long.Parse).ToList();

    var firstTryResult = CheckEquation(arguments, expectedResult, Add, Multiply);

    result1 += firstTryResult;
    result2 += firstTryResult == 0 ? CheckEquation(arguments, expectedResult, Add, Multiply, Concatenate) : firstTryResult;
}

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

static long CheckEquation(IList<long> arguments, long expectedResult, params IEnumerable<Func<long, long, long>> operations)
{
    var stack = new Stack<(int, long)>([(1, arguments[0])]);

    while (stack.TryPop(out var item))
    {
        var (index, accumulator) = item;

        if (index < arguments.Count)
        {
            foreach (var result in operations.Select(o => o(accumulator, arguments[index])).Where(r => r <= expectedResult))
            {
                stack.Push((index + 1, result));
            }
        }
        else if (accumulator == expectedResult)
        {
            return expectedResult;
        }
    }

    return 0;
}

static long Add(long x, long y) => x + y;

static long Multiply(long x, long y) => x * y;

static long Concatenate(long x, long y) => long.Parse($"{x}{y}");
