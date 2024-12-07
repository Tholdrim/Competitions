using System.Text.RegularExpressions;

var (result1, result2, enabled) = (0, 0, true);
var input = await File.ReadAllTextAsync("Input 03.txt");

foreach (var match in Regex.Matches(input).OfType<Match>())
{
    if (match.Groups["instruction"].Success)
    {
        enabled = match.Groups["instruction"].Value == "do()";

        continue;
    }

    var product = int.Parse(match.Groups["x"].Value) * int.Parse(match.Groups["y"].Value);

    result1 += product;
    result2 += enabled ? product : 0;
}

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

partial class Program
{
    [GeneratedRegex(@"mul\((?<x>\d{1,3}),(?<y>\d{1,3})\)|(?<instruction>do\(\)|don't\(\))")]
    private static partial Regex Regex { get; }
}
