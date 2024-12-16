var lines = await File.ReadAllLinesAsync("Input 15.txt");
var emptyLineIndex = lines.Index().Where(p => p.Item.Length == 0).Select(p => p.Index).First();

var map1 = lines.Take(emptyLineIndex).Select(l => l.ToArray()).ToArray();
var map2 = map1.Select(l => l.SelectMany(c => c == '#' ? "##" : (c == 'O' ? "[]" : (c == '.' ? ".." : "@."))).ToArray()).ToArray();
var moves = lines.Skip(emptyLineIndex + 1).Aggregate((r, l) => $"{r}{l.Replace("\n", "")}");

var (result1, result2) = (PerformMovementSequence(map1), PerformMovementSequence(map2));

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

int PerformMovementSequence(char[][] map)
{
    var position = Vector2D.GetAllPositions(map).First(p => map[p.Y][p.X] == '@');

    map[position.Y][position.X] = '.';

    foreach (var direction in moves.Select(Vector2D.FromMovementSymbol))
    {
        var newPosition = new Vector2D(position.X + direction.X, position.Y + direction.Y);

        switch (map[newPosition.Y][newPosition.X])
        {
            case '.':
                position = newPosition;
                break;

            case 'O':
                position = TryToMoveBoxes(map, newPosition, direction) ? newPosition : position;
                break;

            case '[' or ']':
                position = TryToMoveBigBoxes(map, newPosition, direction, []) ? newPosition : position;
                break;
        }
    }

    return Vector2D.GetAllPositions(map).Sum(p => map[p.Y][p.X] is 'O' or '[' ? p.X + 100 * p.Y : 0);
}

static bool TryToMoveBoxes(char[][] map, Vector2D firstBoxPosition, Vector2D direction)
{
    var position = new Vector2D(firstBoxPosition.X + direction.X, firstBoxPosition.Y + direction.Y);

    while (map[position.Y][position.X] == 'O')
    {
        position = new Vector2D(position.X + direction.X, position.Y + direction.Y);
    }

    if (map[position.Y][position.X] == '#')
    {
        return false;
    }

    map[position.Y][position.X] = 'O';
    map[firstBoxPosition.Y][firstBoxPosition.X] = '.';

    return true;
}

static bool TryToMoveBigBoxes(char[][] map, Vector2D position, Vector2D delta, HashSet<Vector2D> visited, bool isFirst = true)
{
    if (map[position.Y][position.X] == '#')
    {
        return false;
    }

    if (map[position.Y][position.X] == '.')
    {
        return true;
    }

    if (delta.Y == 0)
    {
        var nextPosition = new Vector2D(position.X + delta.X, position.Y);
        var result = TryToMoveBigBoxes(map, nextPosition, delta, visited, false);

        if (result)
        {
            map[nextPosition.Y][nextPosition.X] = map[position.Y][position.X];

            if (isFirst)
            {
                map[position.Y][position.X] = '.';
            }
        }

        return result;
    }
    else if (map[position.Y][position.X] == '[')
    {
        var nextPositionLeft = new Vector2D(position.X, position.Y + delta.Y);
        var nextPositionRight = new Vector2D(position.X + 1, position.Y + delta.Y);

        var result = TryToMoveBigBoxes(map, nextPositionLeft, delta, visited, false)
            && TryToMoveBigBoxes(map, nextPositionRight, delta, visited, false);

        if (result)
        {
            visited.Add(nextPositionLeft);

            if (isFirst)
            {
                foreach (var leftPosition in visited)
                {
                    map[leftPosition.Y][leftPosition.X] = '[';
                    map[leftPosition.Y][leftPosition.X + 1] = ']';
                    map[leftPosition.Y - delta.Y][leftPosition.X] = '.';
                    map[leftPosition.Y - delta.Y][leftPosition.X + 1] = '.';
                }

                map[position.Y][position.X] = '.';
                map[position.Y][position.X + 1] = '.';
            }
        }

        return result;
    }

    return TryToMoveBigBoxes(map, new Vector2D(position.X - 1, position.Y), delta, visited, isFirst);
}

internal record Vector2D(int X, int Y)
{
    public static Vector2D FromMovementSymbol(char move) => move switch
    {
        '<' => new(-1, 0),
        '^' => new(0, -1),
        '>' => new(1, 0),
        'v' => new(0, 1),
        _ => throw new NotImplementedException()
    };

    public static IEnumerable<Vector2D> GetAllPositions(char[][] map)
    {
        return Enumerable.Range(1, map.Length - 1).SelectMany(y => Enumerable.Range(1, map[0].Length - 1), (y, x) => new Vector2D(x, y));
    }
}
