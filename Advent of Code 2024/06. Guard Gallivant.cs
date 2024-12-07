var map = File.ReadAllLines("Input 06.txt").Select(r => $"*{r}*").ToList();
var borderRow = new string('*', map[0].Length);

map = [borderRow, .. map, borderRow];

var startingPosition = Enumerable.Range(1, map.Count - 1)
    .SelectMany(y => Enumerable.Range(1, borderRow.Length - 1), (y, x) => new Vector2D(x, y))
    .First(p => map[p.Y][p.X] == '^');

var (result1, result2) = PerformGuardPatrol(startingPosition, new Vector2D(0, -1));

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

PatrolResult PerformGuardPatrol(Vector2D position, Vector2D direction, Vector2D? obstructionPosition = null)
{
    var obstructionPositions = new List<Vector2D>();
    var guardMovements = new Dictionary<Vector2D, List<Vector2D>>();

    while (map[position.Y][position.X] != '*' && !guardMovements.GetValueOrDefault(position, []).Contains(direction))
    {
        var newPosition = new Vector2D(position.X + direction.X, position.Y + direction.Y);

        guardMovements[position] = [.. guardMovements.GetValueOrDefault(position, []), direction];

        if (newPosition == obstructionPosition || map[newPosition.Y][newPosition.X] == '#')
        {
            direction = direction.RotateVector();

            continue;
        }

        if (obstructionPosition == null && !guardMovements.ContainsKey(newPosition))
        {
            var updatedPatrolResult = PerformGuardPatrol(position, direction.RotateVector(), obstructionPosition: newPosition);

            if (updatedPatrolResult.IsLooped)
            {
                obstructionPositions.Add(newPosition);
            }
        }

        position = newPosition;
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
}
