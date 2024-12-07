var (result1, result2) = (0, 0);

var lines = await File.ReadAllLinesAsync("Input 05.txt");
var emptyRowIndex = lines.Index().Where(p => string.IsNullOrEmpty(p.Item)).Select(p => p.Index).First();

var pageOrderingRules = lines
    .Take(emptyRowIndex)
    .Select(l => l.Split('|'))
    .ToLookup(d => int.Parse(d[0]), d => int.Parse(d[1]));

var updates = lines
    .Skip(emptyRowIndex + 1)
    .Select(l => l.Split(',').Select(int.Parse).ToList());

foreach (var update in updates)
{
    if (IsUpdateInCorrectOrder(update))
    {
        result1 += update[update.Count / 2];
    }
    else
    {
        var correctlyOrderedUpdate = FixUpdate(update);

        result2 += correctlyOrderedUpdate[correctlyOrderedUpdate.Count / 2];
    }
}

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

bool IsUpdateInCorrectOrder(IList<int> update)
{
    for (var i = 1; i < update.Count; ++i)
    {
        if (Enumerable.Range(0, i).Any(j => pageOrderingRules[update[i]].Contains(update[j])))
        {
            return false;
        }
    }

    return true;
}

IList<int> FixUpdate(IList<int> update)
{
    var result = new List<int>(update.Count);
    var queue = new Queue<int>(update);

    while (queue.TryDequeue(out var page))
    {
        var index = pageOrderingRules[page].Select(p => result.IndexOf(p)).Max();

        result.Insert(index + 1, page);
    }

    return result;
}
