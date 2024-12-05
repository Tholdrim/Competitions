namespace AdventOfCode2024
{
    [TestClass]
    public class Day04
    {
        private static readonly IEnumerable<(int X, int Y)> Directions = [(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)];

        [TestMethod]
        [DataRow("Sample 04.txt", 18, 9, DisplayName = "Sample")]
        [DataRow("Input 04.txt", 2504, 1923, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2)
        {
            var (result1, result2) = (0, 0);

            var grid = File.ReadAllLines(fileName).Select(r => '*' + r + '*').ToArray();
            var borderRow = new string('*', grid[0].Length);

            grid = [borderRow, .. grid, borderRow];

            for (var y = 1; y < grid.Length - 1; ++y)
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

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);

            int CountXmasOccurrences(int x, int y) => Directions.Count(d => grid[y + 1 * d.Y][x + 1 * d.X] == 'M'
                                                                         && grid[y + 2 * d.Y][x + 2 * d.X] == 'A'
                                                                         && grid[y + 3 * d.Y][x + 3 * d.X] == 'S');

            bool IsPartOfXMas(int x, int y) => (grid[y - 1][x - 1], grid[y + 1][x + 1]) is ('M', 'S') or ('S', 'M')
                                            && (grid[y + 1][x - 1], grid[y - 1][x + 1]) is ('M', 'S') or ('S', 'M');
        }
    }
}
