namespace AdventOfCode2025
{
    [TestClass]
    public class Day04
    {
        [TestMethod]
        [DataRow("Sample 04.txt", 13, 43, DisplayName = "Sample")]
        [DataRow("Input 04.txt", 1451, 8701, DisplayName = "Input")]
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
                    if (grid[y][x] != '@')
                    {
                        continue;
                    }

                    if (CanBeRemoved(grid, x, y))
                    {
                        ++result1;
                    }
                }
            }

            var lastResult = -1;

            while (lastResult != result2)
            {
                lastResult = result2;

                for (var y = 1; y < grid.Length - 1; ++y)
                {
                    for (var x = 1; x < grid[y].Length - 1; ++x)
                    {
                        if (grid[y][x] != '@')
                        {
                            continue;
                        }

                        if (CanBeRemoved(grid, x, y))
                        {
                            var row = grid[y].ToArray();

                            row[x] = '.';
                            grid[y] = new string(row);
                            ++result2;
                        }
                    }
                }
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static bool CanBeRemoved(string[] grid, int x, int y)
        {
            var count = 0;

            foreach (var direction in s_directions.Span)
            {
                if (grid[y + direction.Y][x + direction.X] == '@')
                {
                    ++count;
                }
            }

            return count < 4;
        }

        private static readonly ReadOnlyMemory<Vector2D> s_directions = new Vector2D[]
        {
            new(-1, -1), new(-1, 0), new(-1, 1), new(0, -1), new(0, 1), new(1, -1), new(1, 0), new(1, 1)
        };

        private readonly record struct Vector2D(int X, int Y);
    }
}
