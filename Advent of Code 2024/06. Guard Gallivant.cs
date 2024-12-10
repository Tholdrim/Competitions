var map = File.ReadAllLines("Input 06.txt").Select(r => $"*{r}*").ToArray();
var borderRow = new string('*', map[0].Length);

map = [borderRow, .. map, borderRow];

var startingPosition = Vector2D.GetAllPositions(map).First(p => map[p.Y][p.X] == '^');
var (result1, result2) = PerformGuardPatrol(startingPosition, new Vector2D(0, -1));

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

PatrolResult PerformGuardPatrol(Vector2D position, Vector2D direction, Vector2D? obstructionPosition = null)
{
    var obstructionPositions = new List<Vector2D>();
    var guardMovements = new Dictionary<Vector2D, List<Vector2D>>();

    while (map[position.Y][position.X] != '*' && !guardMovements.GetValueOrDefault(position, []).Contains(direction))
    {
        var nextPosition = position + direction;

        guardMovements[position] = [.. guardMovements.GetValueOrDefault(position, []), direction];

        if (nextPosition == obstructionPosition || map[nextPosition.Y][nextPosition.X] == '#')
        {
            direction = direction.RotateVector();

            continue;
        }

        if (obstructionPosition == null && !guardMovements.ContainsKey(nextPosition))
        {
            var updatedPatrolResult = PerformGuardPatrol(position, direction.RotateVector(), obstructionPosition: nextPosition);

            if (updatedPatrolResult.IsLooped)
            {
                obstructionPositions.Add(nextPosition);
            }
        }

        position = nextPosition;
    }

    return new(guardMovements.Keys.Count, obstructionPositions.Count)
    {
        IsLooped = guardMovements.GetValueOrDefault(position, []).Contains(direction)
    };
}

internal record PatrolResult(int Positions, int Obstructions)
{
    public bool IsLooped { get; init; }
}

internal record Vector2D(int X, int Y)
{
    public Vector2D RotateVector() => new(-Y, X);

    public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);

    public static IEnumerable<Vector2D> GetAllPositions(string[] map)
    {
        return Enumerable.Range(1, map.Length - 1).SelectMany(y => Enumerable.Range(1, map[0].Length - 1), (y, x) => new Vector2D(x, y));
    }
}
