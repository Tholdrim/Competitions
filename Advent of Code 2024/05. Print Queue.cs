var (result1, result2) = (0, 0);

var lines = File.ReadAllLines("Input 05.txt");
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

        continue;
    }

    var sortedUpdate = SortUpdate(update);

    result2 += sortedUpdate[sortedUpdate.Count / 2];
}

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

bool IsUpdateInCorrectOrder(List<int> update)
{
    return Enumerable.Range(1, update.Count - 1)
        .All(i => Enumerable.Range(0, i)
            .All(j => !pageOrderingRules[update[i]].Contains(update[j])));
}

IList<int> SortUpdate(List<int> update)
{
    var result = new List<int>(update.Count);

    update.ForEach(p => result.Insert(pageOrderingRules[p].Max(result.IndexOf) + 1, p));

    return result;
}
