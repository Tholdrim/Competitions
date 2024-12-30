namespace AdventOfCode2024
{
    [TestClass]
    public class Day08
    {
        [TestMethod]
        [DataRow("Sample 08.txt", 14, 34, DisplayName = "Sample")]
        [DataRow("Input 08.txt", 354, 1263, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2)
        {
            var strictAntinodes = new HashSet<Vector2D>();
            var harmonicAntinodes = new HashSet<Vector2D>();

            var lines = File.ReadAllLines(fileName);

            var antennaGroups = Enumerable.Range(0, lines.Length)
                .SelectMany(y => Enumerable.Range(0, lines[y].Length), (y, x) => new { Antenna = lines[y][x], Position = new Vector2D(x, y) })
                .Where(e => e.Antenna != '.')
                .GroupBy(e => e.Antenna, e => e.Position);

            foreach (var (position, offset) in antennaGroups.SelectMany(g => ComputeAntinodeOffsets([.. g])))
            {
                var antinodePosition = position + offset;

                for (var i = 0; antinodePosition.IsValidPosition(width: lines[0].Length, height: lines.Length); ++i)
                {
                    harmonicAntinodes.Add(antinodePosition);

                    if (i == 1)
                    {
                        strictAntinodes.Add(antinodePosition);
                    }

                    antinodePosition += offset;
                }
            }

            var result1 = strictAntinodes.Count;
            var result2 = harmonicAntinodes.Count;

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static IEnumerable<(Vector2D Position, Vector2D Offset)> ComputeAntinodeOffsets(Vector2D[] positions)
        {
            return positions.SelectMany((_, i) => SkipElement(i), (p1, p2) => (p1, p2 - p1));

            Vector2D[] SkipElement(int index) => [.. positions[..index], .. positions[(index + 1)..]];
        }

        private readonly record struct Vector2D(int X, int Y)
        {
            public bool IsValidPosition(int width, int height) => 0 <= X && X < width && 0 <= Y && Y < height;

            public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);

            public static Vector2D operator -(Vector2D a, Vector2D b) => new(a.X - b.X, a.Y - b.Y);
        }
    }
}
