var (result1, result2) = (0, 0);

foreach (var line in File.ReadLines("Input 02.txt"))
{
    var levels = line.Split(' ').Select(int.Parse).ToList();
    var isReportSafe = IsReportSafe(levels);

    result1 += isReportSafe ? 1 : 0;
    result2 += isReportSafe || IsReportSafeWithProblemDampener(levels) ? 1 : 0;
}

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

static bool IsReportSafeWithProblemDampener(List<int> levels)
{
    return Enumerable.Range(0, levels.Count)
        .Select(i => levels.Where((_, j) => i != j).ToList())
        .Any(IsReportSafe);
}

static bool IsReportSafe(List<int> levels)
{
    var delta = levels[1] - levels[0];

    if (delta == 0 || Math.Abs(delta) > 3)
    {
        return false;
    }

    return Enumerable.Range(1, levels.Count - 1)
        .Select(i => levels[i] - levels[i - 1])
        .All(d => d * delta > 0 && Math.Abs(d) <= 3);
}
