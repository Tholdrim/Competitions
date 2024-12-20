var map = File.ReadAllLines("Input 20.txt");
var startTilePosition = Vector2D.GetAllPositions(map).Single(p => map[p.Y][p.X] == 'S');

IEnumerable<Vector2D> directions = [new(-1, 0), new(1, 0), new(0, 1), new(0, -1)];

var result1 = FindCheats(100, 2);
var result2 = FindCheats(100, 20);

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

List<Vector2D> FindNormalPath()
{
    var queue = new Queue<List<Vector2D>>([[startTilePosition]]);

    while (queue.TryDequeue(out var path))
    {
        var position = path.Last();

        if (map[position.Y][position.X] == 'E')
        {
            return path;
        }

        foreach (var direction in directions)
        {
            var nextPosition = position + direction;
            var previousPosition = path.Count > 1 ? path[^2] : new Vector2D(0, 0);

            if (nextPosition == previousPosition || map[nextPosition.Y][nextPosition.X] == '#')
            {
                continue;
            }

            queue.Enqueue([.. path, nextPosition]);
        }
    }

    return [];
}

int FindCheats(int picosecondsSaved, int cheatLength)
{
    var result = 0;
    var normalPath = FindNormalPath();

    for (var i = 0; i < normalPath.Count; ++i)
    {
        for (var j = i + 1; j < normalPath.Count; ++j)
        {
            var length = Math.Abs(normalPath[j].X - normalPath[i].X) + Math.Abs(normalPath[j].Y - normalPath[i].Y);

            if (length <= cheatLength && j - i - length >= picosecondsSaved)
            {
                ++result;
            }
        }
    }

    return result;
}

internal record Vector2D(int X, int Y)
{
    public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);

    public static IEnumerable<Vector2D> GetAllPositions(string[] map)
    {
        return Enumerable.Range(1, map.Length - 1).SelectMany(y => Enumerable.Range(1, map[0].Length - 1), (y, x) => new Vector2D(x, y));
    }
}
