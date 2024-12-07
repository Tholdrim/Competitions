var (result1, result2) = (0, 0);
IEnumerable<(int X, int Y)> directions = [(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)];

var grid = File.ReadAllLines("Input 04.txt").Select(r => $"*{r}*").ToList();
var borderRow = new string('*', grid[0].Length);

grid = [borderRow, .. grid, borderRow];

for (var y = 1; y < grid.Count - 1; ++y)
{
    for (var x = 1; x < borderRow.Length - 1; ++x)
    {
        switch (grid[y][x])
        {
            case 'X':
                result1 += CountXmasOccurrences(x, y);
                break;

            case 'A' when IsPartOfXMas(x, y):
                ++result2;
                break;
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
