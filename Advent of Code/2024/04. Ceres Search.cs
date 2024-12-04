namespace AdventOfCode.Year2024
{
    [TestClass]
    public class Day04
    {
        private static readonly Memory<(int, int)> Directions = new[] { (-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1) };

        [TestMethod]
        [DataRow("Data/Sample 04.txt", 18, 9, DisplayName = "Sample")]
        [DataRow("Data/Input 04.secret", 2504, 1923, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2)
        {
            var (result1, result2) = (0, 0);

            var grid = File.ReadLines(fileName).Select(l => "\0" + l + "\0").ToArray();
            var borderRow = new string('\0', grid[0].Length);

            grid = [borderRow, .. grid, borderRow];

            for (var y = 1; y < grid.Length - 1; ++y)
            {
                for (var x = 1; x < grid[y].Length - 1; ++x)
                {
                    switch (grid[y][x])
                    {
                        case 'X':
                            result1 += CountXmasOccurrences(grid, x, y);
                            break;

                        case 'A' when IsPartOfXMas(x, y):
                            ++result2;
                            break;
                    }
                }
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);

            bool IsPartOfXMas(int x, int y) => (grid[y - 1][x - 1], grid[y + 1][x + 1]) is ('M', 'S') or ('S', 'M')
                                            && (grid[y + 1][x - 1], grid[y - 1][x + 1]) is ('M', 'S') or ('S', 'M');
        }

        private static int CountXmasOccurrences(ReadOnlySpan<string> grid, int x, int y)
        {
            var count = 0;

            foreach (var (dx, dy) in Directions.Span)
            {
                count += grid[y + dy][x + dx] == 'M' && grid[y + 2 * dy][x + 2 * dx] == 'A' && grid[y + 3 * dy][x + 3 * dx] == 'S' ? 1 : 0;
            }

            return count;
        }
    }
}
