var strictAntinodes = new HashSet<Vector2D>();
var harmonicAntinodes = new HashSet<Vector2D>();

var lines = File.ReadAllLines("Input 08.txt");

var antennaGroups = lines.Index()
    .SelectMany(l => l.Item.Index(), (l, c) => new { Antenna = c.Item, Position = new Vector2D(c.Index, l.Index) })
    .Where(e => e.Antenna != '.')
    .GroupBy(e => e.Antenna, e => e.Position);

foreach (var (position, delta) in antennaGroups.SelectMany(GetDeltas))
{
    var antinodePosition = position + delta;

    for (var i = 0; antinodePosition.IsValidPosition(width: lines[0].Length, height: lines.Length); ++i)
    {
        harmonicAntinodes.Add(antinodePosition);

        if (i == 1)
        {
            strictAntinodes.Add(antinodePosition);
        }

        antinodePosition += delta;
    }
}

var result1 = strictAntinodes.Count;
var result2 = harmonicAntinodes.Count;

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

IEnumerable<(Vector2D Position, Vector2D Delta)> GetDeltas(IEnumerable<Vector2D> positions)
{
    return positions.SelectMany(p => positions.Except([p]), (p1, p2) => (Position: p1, Delta: p2 - p1));
}

record Vector2D(int X, int Y)
{
    public bool IsValidPosition(int width, int height) => 0 <= X && X < width && 0 <= Y && Y < height;

    public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);

    public static Vector2D operator -(Vector2D a, Vector2D b) => new(a.X - b.X, a.Y - b.Y);
}
