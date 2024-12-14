
var (result1, result2) = (0, 0);

var map = File.ReadAllLines("Input 10.txt").Select(r => $"*{r}*").ToList();
var borderRow = new string('*', map[0].Length);

map = [borderRow, .. map, borderRow];

for (var y = 1; y < map.Count - 1; ++y)
{
    for (var x = 1; x < borderRow.Length - 1; ++x)
    {
        switch (map[y][x])
        {
            case '0':
                result1 += CountTrailheadScore(x, y);
                result2 += CountTrailheadRating(x, y);
                break;
        }
    }
}

int CountTrailheadScore(int x, int y)
{
    var result = 0;
    var visited = new HashSet<(int, int)>();
    var queue = new Queue<(int, int, char)>([(x, y, '0')]);

    while (queue.TryDequeue(out var item))
    {
        var (x2, y2, height) = item;

        if (visited.Contains((x2, y2)))
        {
            continue;
        }

        visited.Add((x2, y2));

        if (height == '9')
        {
            ++result;
            continue;
        }

        if (map[y2][x2 - 1] == height + 1)
        {
            queue.Enqueue((x2 - 1, y2, (char)(height + 1)));
        }
        if (map[y2][x2 + 1] == height + 1)
        {
            queue.Enqueue((x2 + 1, y2, (char)(height + 1)));
        }
        if (map[y2 - 1][x2] == height + 1)
        {
            queue.Enqueue((x2, y2 - 1, (char)(height + 1)));
        }
        if (map[y2 + 1][x2] == height + 1)
        {
            queue.Enqueue((x2, y2 + 1, (char)(height + 1)));
        }
    }

    return result;
}

int CountTrailheadRating(int x, int y)
{
    var result = 0;
    var queue = new Queue<(int, int, char)>([(x, y, '0')]);

    while (queue.TryDequeue(out var item))
    {
        var (x2, y2, height) = item;

        if (height == '9')
        {
            ++result;
            continue;
        }

        if (map[y2][x2 - 1] == height + 1)
        {
            queue.Enqueue((x2 - 1, y2, (char)(height + 1)));
        }
        if (map[y2][x2 + 1] == height + 1)
        {
            queue.Enqueue((x2 + 1, y2, (char)(height + 1)));
        }
        if (map[y2 - 1][x2] == height + 1)
        {
            queue.Enqueue((x2, y2 - 1, (char)(height + 1)));
        }
        if (map[y2 + 1][x2] == height + 1)
        {
            queue.Enqueue((x2, y2 + 1, (char)(height + 1)));
        }
    }

    return result;
}

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");
