using System.Text.RegularExpressions;

using Coordinates = System.Collections.Generic.IList<(int X, int Y)>;

var lines = File.ReadAllLines("Input 13.txt");

var (result1, result2) = ParseMachineCoordinates(lines)
    .Select(c => new { Initial = SolveInitialEquations(c), Updated = SolveUpdatedEquations(c) })
    .Aggregate((0, 0L), (r, s) => (r.Item1 + 3 * s.Initial.A + s.Initial.B, r.Item2 + 3L * s.Updated.A + s.Updated.B));

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

static (int A, int B) SolveInitialEquations(Coordinates coordinates)
{
    var (a, b) = ((int, int))SolveEquations(coordinates);

    return 0 <= a && a <= 100 && 0 <= b && b <= 100 ? (a, b) : (0, 0);
}

static (long A, long B) SolveUpdatedEquations(Coordinates coordinates)
{
    return ((long, long))SolveEquations(coordinates, 10_000_000_000_000L);
}

static (double, double) SolveEquations(Coordinates coordinates, long prizeIncrement = 0L)
{
    double determinant = coordinates[0].X * coordinates[1].Y - coordinates[1].X * coordinates[0].Y;

    if (determinant == 0.0)
    {
        return (0.0, 0.0);
    }

    var prize = (X: coordinates[2].X + prizeIncrement, Y: coordinates[2].Y + prizeIncrement);

    var a = (prize.X * coordinates[1].Y - coordinates[1].X * prize.Y) / determinant;
    var b = (coordinates[0].X * prize.Y - prize.X * coordinates[0].Y) / determinant;

    return Math.Abs(a % 1.0) <= double.Epsilon && Math.Abs(b % 1.0) <= double.Epsilon ? (a, b) : (0.0, 0.0);
}

static IEnumerable<Coordinates> ParseMachineCoordinates(string[] lines)
{
    for (var i = 0; i < lines.Length; i += 4)
    {
        IEnumerable<string> singleMachineLines = [lines[i], lines[i + 1], lines[i + 2]];

        yield return singleMachineLines
            .Select(l => CoordinatesRegex.Match(l))
            .Select(m => (X: int.Parse(m.Groups["x"].Value), Y: int.Parse(m.Groups["y"].Value)))
            .ToList();
    }
}

internal partial class Program
{
    [GeneratedRegex(@"(Button [AB]|Prize): X[+=](?<x>\d+), Y[+=](?<y>\d+)")]
    private static partial Regex CoordinatesRegex { get; }
}
