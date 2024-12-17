using System.Text.RegularExpressions;

var input = File.ReadAllText("Input 17.txt");

var registers = new Registers(0L, 0L, 0L);
var program = ProgramRegex.Match(input).Groups["program"].Value.Split(',').Select(int.Parse).ToList();

foreach (var match in RegisterRegex.Matches(input).OfType<Match>())
{
    var value = long.Parse(match.Groups["value"].Value);

    registers = match.Groups["register"].Value switch
    {
        "A" => registers with { A = value },
        "B" => registers with { B = value },
        "C" => registers with { C = value },
        _   => throw new NotImplementedException()
    };
}

var result1 = string.Join(",", RunProgram(program, registers));
var result2 = FindRegisterValueBeforeCorruption(program, registers);

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

static IList<int> RunProgram(List<int> program, Registers registers)
{
    var result = new List<int>();

    for (var i = 0; i < program.Count; i += 2)
    {
        var literalOperand = program[i + 1];
        var comboOperand = GetComboOperand(literalOperand, registers);

        switch (program[i])
        {
            case 0:
                registers = registers with { A = registers.A >> (int)comboOperand };
                break;

            case 1:
                registers = registers with { B = registers.B ^ literalOperand };
                break;

            case 2:
                registers = registers with { B = comboOperand & 0b111L };
                break;

            case 3 when registers.A != 0L:
                i = literalOperand - 2;
                break;

            case 4:
                registers = registers with { B = registers.B ^ registers.C };
                break;

            case 5:
                result.Add((int)comboOperand & 0b111);
                break;

            case 6:
                registers = registers with { B = registers.A >> (int)comboOperand };
                break;

            case 7:
                registers = registers with { C = registers.A >> (int)comboOperand };
                break;
        }
    }

    return result;
}

static long FindRegisterValueBeforeCorruption(List<int> program, Registers registers)
{
    var result = 1L;

    for (var i = 0; i < program.Count; ++i)
    {
        var outputMatched = false;

        for (result = (result - 1L) * 8L; !outputMatched; ++result)
        {
            var output = RunProgram(program, registers with { A = result });

            outputMatched = program.Take(^output.Count..).SequenceEqual(output);
        }
    }

    return result - 1L;
}

static long GetComboOperand(int literalOperand, Registers registers) => literalOperand switch
{
    >= 0 and <= 3 => literalOperand,
    4             => registers.A,
    5             => registers.B,
    6             => registers.C,
    _             => throw new NotImplementedException()
};

internal record Registers(long A, long B, long C);

internal partial class Program
{
    [GeneratedRegex(@"Program: (?<program>[0-7](,[0-7])+)")]
    private static partial Regex ProgramRegex { get; }

    [GeneratedRegex(@"Register (?<register>[ABC]): (?<value>\d+)")]
    private static partial Regex RegisterRegex { get; }
}
