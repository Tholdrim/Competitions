using Vector2D = (int X, int Y);

var (result1, result2) = (0, 0);
var visited = new HashSet<Point>();

var map = File.ReadAllLines("Input 12.txt").Select(r => $"*{r}*").ToList();
var borderRow = new string('*', map[0].Length);

map = [borderRow, .. map, borderRow];

for (var y = 1; y < map.Count - 1; ++y)
{
    for (var x = 1; x < borderRow.Length - 1; ++x)
    {
        if (visited.Contains(new Point(x, y)))
        {
            continue;
        }

        var (perimeter, sides, regionArea) = AnalyzeRegion(new Point(x, y), map[y][x]);

        result1 += regionArea.Count * perimeter;
        result2 += regionArea.Count * sides;

        visited.UnionWith(regionArea);
    }
}

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

(int Perimeter, int Sides, ISet<Point> RegionArea) AnalyzeRegion(Point startingPoint, char type)
{
    var perimeter = 0;
    var visited = new HashSet<Point>();
    var corners = new HashSet<(Point, double)>();

    var queue = new Queue<Point>([startingPoint]);

    while (queue.TryDequeue(out var point))
    {
        if (visited.Contains(point))
        {
            continue;
        }

        visited.Add(point);

        var (x, y) = point;

        for (var degrees = 0.0; degrees < 360.0; degrees += 90.0)
        {
            var leftUnitVector = RotateVector(new Vector2D(-1, 0), degrees);
            var newPoint = point.Apply(leftUnitVector);

            if (newPoint.ReadMap(map) == type)
            {
                queue.Enqueue(newPoint);

                continue;
            }

            ++perimeter;

            var downUnitVector = RotateVector(new Vector2D(0, 1), degrees);
            var upUnitVector = RotateVector(new Vector2D(0, -1), degrees);

            if (point.Apply(upUnitVector).ReadMap(map) != type)
            {
                corners.Add((newPoint.Apply(upUnitVector), (degrees + 90.0) % 360.0));
            }

            if (newPoint.Apply(downUnitVector).ReadMap(map) == type && point.Apply(downUnitVector).ReadMap(map) == type)
            {
                corners.Add((newPoint, (degrees + 90.0) % 360.0));
            }

            if (newPoint.Apply(upUnitVector).ReadMap(map) == type && point.Apply(upUnitVector).ReadMap(map) == type)
            {
                corners.Add((newPoint, degrees));
            }
        }
    }

    return (perimeter, corners.Count, visited);
}

static Vector2D RotateVector(Vector2D delta, double degrees)
{
    var angle = double.DegreesToRadians(degrees);
    var (sin, cos) = ((int)Math.Sin(angle), (int)Math.Cos(angle));

    return new Vector2D(delta.X * cos - delta.Y * sin, delta.X * sin + delta.Y * cos);
}

internal readonly record struct Point(int X, int Y)
{
    public Point Apply(Vector2D vector) => this with { X = X + vector.X, Y = Y + vector.Y };

    public char ReadMap(IList<string> map) => map[Y][X];
}
