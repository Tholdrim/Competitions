var (result1, result2) = (0, 0L);

var lines = File.ReadAllLines("Input 19.txt");
var patterns = lines[0].Split(", ");

foreach (var design in lines.Skip(2))
{
    var possibilites = CountPossibilities(design, []);

    result1 += possibilites > 0 ? 1 : 0;
    result2 += possibilites;
}

long CountPossibilities(ReadOnlySpan<char> design, Dictionary<int, long> cache)
{
    if (design.Length == 0)
    {
        return 1;
    }

    if (cache.TryGetValue(design.Length, out var result))
    {
        return result;
    }

    foreach (var pattern in patterns)
    {
        if (design.StartsWith(pattern))
        {
            result += CountPossibilities(design[pattern.Length..], cache);
        }
    }

    return cache[design.Length] = result;
}

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");
