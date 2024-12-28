namespace AdventOfCode2024
{
    [TestClass]
    public class Day10
    {
        private static readonly IEnumerable<Vector2D> Directions = [new(-1, 0), new(1, 0), new(0, -1), new(0, 1)];

        [TestMethod]
        [DataRow("Sample 10.txt", 36, 81, DisplayName = "Sample")]
        [DataRow("Input 10.txt", 737, 1619, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2)
        {
            var (result1, result2) = (0, 0);

            var map = File.ReadLines(fileName).Select(l => "*" + l + "*").ToArray();
            var borderRow = new string('*', map[0].Length);

            map = [borderRow, .. map, borderRow];

            for (var y = 1; y < map.Length - 1; ++y)
            {
                for (var x = 1; x < map[y].Length - 1; ++x)
                {
                    if (map[y][x] == '0')
                    {
                        var (score, rating) = CalculateTrailheadMetrics(map, x, y);

                        result1 += score;
                        result2 += rating;
                    }
                }
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static (int Score, int Rating) CalculateTrailheadMetrics(string[] map, int x, int y)
        {
            var (score, rating) = (0, 0);

            var visitedPeaks = new HashSet<Vector2D>();
            var stack = new Stack<(Vector2D Position, char Height)>([(new(x, y), '0')]);

            while (stack.TryPop(out var item))
            {
                if (item.Height == '9')
                {
                    score += visitedPeaks.Add(item.Position) ? 1 : 0;
                    ++rating;

                    continue;
                }

                var nextHeight = (char)(item.Height + 1);

                foreach (var direction in Directions)
                {
                    var nextPosition = item.Position + direction;

                    if (map[nextPosition.Y][nextPosition.X] == nextHeight)
                    {
                        stack.Push(new(nextPosition, nextHeight));
                    }
                }
            }

            return (score, rating);
        }

        private readonly record struct Vector2D(int X, int Y)
        {
            public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);
        }
    }
}
