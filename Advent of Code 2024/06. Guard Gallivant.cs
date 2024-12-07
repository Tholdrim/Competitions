using Vector2D = (int X, int Y);
using PatrolResult = (int Positions, int Obstructions, bool IsLooped);

var map = File.ReadAllLines("Input 06.txt").Select(r => $"*{r}*").ToList();
var borderRow = new string('*', map[0].Length);

map.Insert(0, borderRow);
map.Add(borderRow);

var delta = new Vector2D(0, -1);
var position = Enumerable.Range(1, map.Count - 1)
    .SelectMany(y => Enumerable.Range(1, borderRow.Length - 1), (y, x) => new Vector2D(x, y))
    .First(position => map[position.Y][position.X] == '^');

var (result1, result2, _) = PerformGuardPatrol(position, delta);

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

PatrolResult PerformGuardPatrol(Vector2D position, Vector2D delta, Vector2D? obstructionPosition = null)
{
    var obstructionPositions = new List<Vector2D>();
    var guardMovements = new Dictionary<Vector2D, IList<Vector2D>>();

    while (map[position.Y][position.X] != '*' && !guardMovements.GetValueOrDefault(position, []).Contains(delta))
    {
        var newPosition = new Vector2D(position.X + delta.X, position.Y + delta.Y);

        guardMovements[position] = [.. guardMovements.GetValueOrDefault(position, []), delta];

        if (newPosition == obstructionPosition || map[newPosition.Y][newPosition.X] == '#')
        {
            delta = RotateVector(delta);

            continue;
        }

        if (obstructionPosition == null && !guardMovements.ContainsKey(newPosition))
        {
            var updatedPatrolResult = PerformGuardPatrol(position, RotateVector(delta), obstructionPosition: newPosition);

            if (updatedPatrolResult.IsLooped)
            {
                obstructionPositions.Add(newPosition);
            }
        }

        position = newPosition;
    }

    return
    (
        Positions: guardMovements.Keys.Count,
        Obstructions: obstructionPositions.Count,
        IsLooped: guardMovements.GetValueOrDefault(position, []).Contains(delta)
    );
}

static Vector2D RotateVector(Vector2D delta) => (-delta.Y, delta.X);
