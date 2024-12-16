var map = await File.ReadAllLinesAsync("Input 16.txt");
var startTilePosition = Vector2D.GetAllPositions(map).First(p => map[p.Y][p.X] == 'S');
var endTilePosition = Vector2D.GetAllPositions(map).First(p => map[p.Y][p.X] == 'E');

var (result1, result2) = FindShortestPaths(startTilePosition, endTilePosition);

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

(int, int) FindShortestPaths(Vector2D startTilePosition, Vector2D endTilePosition)
{
    IEnumerable<Vector2D> directions = [new(-1, 0), new(1, 0), new(0, 1), new(0, -1)];

    var lastBestScore = int.MaxValue;
    var bestTiles = new HashSet<Vector2D>();
    var bestScores = new Dictionary<Vector2D, int>();
    var queue = new Queue<(List<Vector2D>, Vector2D, int)>([([startTilePosition], new Vector2D(1, 0), 0)]);

    while (queue.TryDequeue(out var element))
    {
        var position = element.Item1[^1];

        if (map[position.Y][position.X] == '#' ||
            bestScores.TryGetValue(position, out var bestTileScore) && bestTileScore < element.Item3 - 1000)
        {
            continue;
        }

        if (position == endTilePosition)
        {
            if (lastBestScore > element.Item3)
            {
                bestTiles.Clear();
                lastBestScore = element.Item3;
            }

            if (lastBestScore == element.Item3)
            {
                bestTiles.UnionWith(element.Item1);
            }
        }

        bestScores[position] = element.Item3;

        foreach (var direction in directions.Where(d => d != new Vector2D(-element.Item2.X, -element.Item2.Y)))
        {
            List<Vector2D> path = [.. element.Item1, new Vector2D(position.X + direction.X, position.Y + direction.Y)];

            queue.Enqueue((path, direction, element.Item3 + (element.Item2 == direction ? 1 : 1001)));
        }
    }

    return (lastBestScore, bestTiles.Count);
}

internal record Vector2D(int X, int Y)
{
    public static IEnumerable<Vector2D> GetAllPositions(string[] map)
    {
        return Enumerable.Range(1, map.Length - 1).SelectMany(y => Enumerable.Range(1, map[0].Length - 1), (y, x) => new Vector2D(x, y));
    }
}
