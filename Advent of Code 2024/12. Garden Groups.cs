var (result1, result2) = (0, 0);

var map = File.ReadAllLines("Input 12.txt").Select(r => $"*{r}*").ToList();
var borderRow = new string('*', map[0].Length);

map = [borderRow, .. map, borderRow];

var visited = new HashSet<(int, int)>();

for (var y = 1; y < map.Count - 1; ++y)
{
    for (var x = 1; x < borderRow.Length - 1; ++x)
    {
        if (visited.Contains((x, y)))
        {
            continue;
        }

        var corners = new HashSet<(int, int, char)>();
        var (area, perimeter) = CalculateRegion(x, y, map[y][x], corners);

        result1 += area * perimeter;
        result2 += area * corners.Count;
    }
}

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

(int, int) CalculateRegion(int x, int y, char type, ISet<(int, int, char)> corners)
{
    if (visited.Contains((x, y)))
    {
        return (0, 0);
    }

    var area = 1;
    var perimeter = 0;

    visited.Add((x, y));

    if (map[y][x - 1] == type)
    {
        var (a, p) = CalculateRegion(x - 1, y, type, corners);
        area += a;
        perimeter += p;
    }
    else
    {
        perimeter++;

        if (map[y - 1][x] != type)
        {
            corners.Add((x - 1, y - 1, '↘'));
        }

        if (map[y - 1][x - 1] == type && map[y - 1][x] == type)
        {
            corners.Add((x - 1, y, '↗'));
        }

        if (map[y + 1][x - 1] == type && map[y + 1][x] == type)
        {
            corners.Add((x - 1, y, '↘'));
        }
    }

    if (map[y][x + 1] == type)
    {
        var (a, p) = CalculateRegion(x + 1, y, type, corners);
        area += a;
        perimeter += p;
    }
    else
    {
        perimeter++;

        if (map[y + 1][x] != type)
        {
            corners.Add((x + 1, y + 1, '↖'));
        }

        if (map[y - 1][x + 1] == type && map[y - 1][x] == type)
        {
            corners.Add((x + 1, y, '↖'));
        }

        if (map[y + 1][x + 1] == type && map[y + 1][x] == type)
        {
            corners.Add((x + 1, y, '↙'));
        }
    }

    if (map[y - 1][x] == type)
    {
        var (a, p) = CalculateRegion(x, y - 1, type, corners);
        area += a;
        perimeter += p;
    }
    else
    {
        perimeter++;

        if (map[y][x + 1] != type)
        {
            corners.Add((x + 1, y - 1, '↙'));
        }

        if (map[y - 1][x - 1] == type && map[y][x - 1] == type)
        {
            corners.Add((x, y - 1, '↙'));
        }

        if (map[y - 1][x + 1] == type && map[y][x + 1] == type)
        {
            corners.Add((x, y - 1, '↘'));
        }
    }

    if (map[y + 1][x] == type)
    {
        var (a, p) = CalculateRegion(x, y + 1, type, corners);
        area += a;
        perimeter += p;
    }
    else
    {
        perimeter++;

        if (map[y][x - 1] != type)
        {
            corners.Add((x - 1, y + 1, '↗'));
        }

        if (map[y + 1][x - 1] == type && map[y][x - 1] == type)
        {
            corners.Add((x, y + 1, '↖'));
        }

        if (map[y + 1][x + 1] == type && map[y][x + 1] == type)
        {
            corners.Add((x, y + 1, '↗'));
        }
    }

    return (area, perimeter);
}
