using Vector2D = (int X, int Y);

var lines = File.ReadAllLines("Input 08.txt");
var bounds = new Vector2D(lines[0].Length, lines.Length);

var antennaGroups = lines.Index()
    .SelectMany(r => r.Item.Index(), (r, c) => new { Antenna = c.Item, Position = new Vector2D(c.Index, r.Index) })
    .Where(e => e.Antenna != '.')
    .GroupBy(e => e.Antenna, e => e.Position);

var strictAntinodes = new HashSet<Vector2D>();
var harmonicAntinodes = new HashSet<Vector2D>();

foreach (var (position, delta) in antennaGroups.SelectMany(FindDeltas))
{
    var (x, y) = (position.X + delta.X, position.Y + delta.Y);

    for (var i = 0; 0 <= x && x < bounds.X && 0 <= y && y < bounds.Y; ++i)
    {
        if (i == 1)
        {
            strictAntinodes.Add((x, y));
        }

        harmonicAntinodes.Add((x, y));

        (x, y) = (x + delta.X, y + delta.Y);
    }
}

var (result1, result2) = (strictAntinodes.Count, harmonicAntinodes.Count);

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

IEnumerable<(Vector2D, Vector2D)> FindDeltas(IEnumerable<Vector2D> positions)
{
    return positions.SelectMany(p => positions.Except([p]), (p1, p2) => (p1, (p2.X - p1.X, p2.Y - p1.Y)));
}
