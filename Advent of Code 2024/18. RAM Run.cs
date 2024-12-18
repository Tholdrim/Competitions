var corruptedBytes = new Dictionary<Vector2D, int>();
var lines = File.ReadAllLines("Input 18.txt");

for (var i = 0; i < lines.Length; ++i)
{
    var data = lines[i].Split(',').Select(int.Parse).ToArray();

    corruptedBytes.Add(new Vector2D(data[0], data[1]), i);
}

var range = new Range(1025, corruptedBytes.Count);

while (range.GetOffsetAndLength(corruptedBytes.Count).Length > 1)
{
    var middle = range.Start.Value + (range.End.Value - range.Start.Value) / 2;

    if (FindShortestPath(new Vector2D(70, 70), middle) == -1)
    {
        range = new Range(range.Start.Value, middle);
    }
    else
    {
        range = new Range(middle, range.End.Value);
    }
}

var position = corruptedBytes.Where(p => p.Value == range.GetOffsetAndLength(corruptedBytes.Count).Offset).Select(p => p.Key).Single();

var result1 = FindShortestPath(new Vector2D(70, 70), 1024);
var result2 = $"{position.X},{position.Y}";

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

int FindShortestPath(Vector2D endPosition, int corruptedBytesCount)
{
    var visited = new HashSet<Vector2D>();
    var queue = new Queue<(Vector2D Position, int Steps)>([(new Vector2D(0, 0), 0)]);

    while (queue.TryDequeue(out var item))
    {
        if (item.Position == endPosition)
        {
            return item.Steps;
        }

        if (visited.Contains(item.Position))
        {
            continue;
        }

        visited.Add(item.Position);

        if (item.Position.X > 0 && corruptedBytes.GetValueOrDefault(new Vector2D(item.Position.X - 1, item.Position.Y), int.MaxValue) >= corruptedBytesCount)
        {
            queue.Enqueue((new Vector2D(item.Position.X - 1, item.Position.Y), item.Steps + 1));
        }

        if (item.Position.X < endPosition.X && corruptedBytes.GetValueOrDefault(new Vector2D(item.Position.X + 1, item.Position.Y), int.MaxValue) >= corruptedBytesCount)
        {
            queue.Enqueue((new Vector2D(item.Position.X + 1, item.Position.Y), item.Steps + 1));
        }

        if (item.Position.Y > 0 && corruptedBytes.GetValueOrDefault(new Vector2D(item.Position.X, item.Position.Y - 1), int.MaxValue) >= corruptedBytesCount)
        {
            queue.Enqueue((new Vector2D(item.Position.X, item.Position.Y - 1), item.Steps + 1));
        }

        if (item.Position.Y < endPosition.Y && corruptedBytes.GetValueOrDefault(new Vector2D(item.Position.X, item.Position.Y + 1), int.MaxValue) >= corruptedBytesCount)
        {
            queue.Enqueue((new Vector2D(item.Position.X, item.Position.Y + 1), item.Steps + 1));
        }
    }

    return -1;
}

internal record Vector2D(int X, int Y);
