var (result1, result2) = (0, 0);

await foreach (var line in File.ReadLinesAsync("Input 02.txt"))
{
    var levels = line.Split(' ').Select(int.Parse).ToList();

    if (IsReportSafe(levels))
    {
        ++result1;
        ++result2;
    }
    else if (IsReportSafeWithProblemDampener(levels))
    {
        ++result2;
    }
}

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

static bool IsReportSafeWithProblemDampener(IList<int> levels)
{
    for (var j = 0; j < levels.Count; ++j)
    {
        var modifiedLevels = levels.Where((_, i) => i != j).ToList();

        if (IsReportSafe(modifiedLevels))
        {
            return true;
        }
    }

    return false;
}

static bool IsReportSafe(IList<int> levels)
{
    var delta = levels[1] - levels[0];

    if (delta == 0 || Math.Abs(delta) > 3)
    {
        return false;
    }

    for (var i = 1; i < levels.Count; ++i)
    {
        var newDelta = levels[i] - levels[i - 1];

        if (newDelta * delta <= 0 || Math.Abs(newDelta) > 3)
        {
            return false;
        }
    }

    return true;
}
