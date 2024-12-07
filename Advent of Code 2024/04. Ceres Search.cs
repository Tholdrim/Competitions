var (result1, result2) = (0, 0);
var directions = new (int X, int Y)[] { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

var grid = File.ReadAllLines("Input 04.txt").Select(r => $"*{r}*").ToList();

grid.Insert(0, new string('*', grid[0].Length));
grid.Add(grid[0]);

for (var y = 1; y < grid.Count - 1; ++y)
{
    for (var x = 1; x < grid[y].Length - 1; ++x)
    {
        if (grid[y][x] == 'X')
        {
            result1 += CountXmasOccurrences(x, y);
        }
        else if (grid[y][x] == 'A' && IsPartOfXMas(x, y))
        {
            ++result2;
        }
    }
}

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

int CountXmasOccurrences(int x, int y) => directions.Count(d => grid[y + 1 * d.Y][x + 1 * d.X] == 'M'
                                                             && grid[y + 2 * d.Y][x + 2 * d.X] == 'A'
                                                             && grid[y + 3 * d.Y][x + 3 * d.X] == 'S');

bool IsPartOfXMas(int x, int y) => (grid[y - 1][x - 1], grid[y + 1][x + 1]) is ('M', 'S') or ('S', 'M')
                                && (grid[y + 1][x - 1], grid[y - 1][x + 1]) is ('M', 'S') or ('S', 'M');
