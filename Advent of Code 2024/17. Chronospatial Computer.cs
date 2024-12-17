using System.Text.RegularExpressions;

var input = File.ReadAllText("Input 17.txt");
var registers = new Dictionary<string, long>();

foreach (var match in RegisterRegex.Matches(input).OfType<Match>())
{
    registers.Add(match.Groups["id"].Value, int.Parse(match.Groups["value"].Value));
}

var program = ProgramRegex.Match(input).Groups["program"].Value.Split(',').Select(int.Parse).ToList();

var result1 = string.Join(",", RunProgram(program, registers["A"], registers["B"], registers["C"]));
var result2 = 0L;

for (var i = 0; i < program.Count; ++i)
{
    IList<int> output = [];
    result2 = result2 * 8L - 1;

    do
    {
        ++result2;
        output = RunProgram(program, result2, registers["B"], registers["C"]);
    }
    while (!program.Take(^output.Count..).SequenceEqual(output));
}

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

static IList<int> RunProgram(List<int> program, long registerA, long registerB, long registerC)
{
    var result = new List<int>();
    var registers = new Dictionary<string, long> { ["A"] = registerA, ["B"] = registerB, ["C"] = registerC };

    for (var i = 0; i < program.Count; i += 2)
    {
        var operand = GetComboOperand(program[i + 1], registers);

        switch (program[i])
        {
            case 0:
                registers["A"] /= (int)Math.Pow(2.0, operand);
                break;

            case 1:
                registers["B"] ^= program[i + 1];
                break;

            case 2:
                registers["B"] = operand % 8;
                break;

            case 3 when registers["A"] != 0:
                i = program[i + 1] - 2;
                break;

            case 4:
                registers["B"] ^= registers["C"];
                break;

            case 5:
                result.Add((int)(operand % 8));
                break;

            case 6:
                registers["B"] = registers["A"] / (int)Math.Pow(2.0, operand);
                break;

            case 7:
                registers["C"] = registers["A"] / (int)Math.Pow(2.0, operand);
                break;
        }
    }

    return result;
}

static long GetComboOperand(int value, Dictionary<string, long> registers) => value switch
{
    0 or 1 or 2 or 3 => value,
    4                => registers["A"],
    5                => registers["B"],
    6                => registers["C"],
    _                => throw new NotImplementedException()
};

internal partial class Program
{
    [GeneratedRegex(@"Register (?<id>[ABC]): (?<value>\d+)")]
    private static partial Regex RegisterRegex { get; }

    [GeneratedRegex(@"Program: (?<program>[0-7](,[0-7])*)")]
    private static partial Regex ProgramRegex { get; }
}
