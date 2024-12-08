using System.Runtime.InteropServices;

namespace AdventOfCode.Year2024
{
    [TestClass]
    public class Day08
    {
        [TestMethod]
        [DataRow("Data/Sample 08.txt", 14, 34, DisplayName = "Sample")]
        [DataRow("Data/Input 08.secret", 354, 1263, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2)
        {
            var lines = File.ReadAllLines(fileName);
            var mapSize = lines.Length * lines[0].Length;

            Span<bool> strictAntinodes = stackalloc bool[mapSize];
            Span<bool> harmonicAntinodes = stackalloc bool[mapSize];

            strictAntinodes.Clear();
            harmonicAntinodes.Clear();

            foreach (var (position, offset) in ComputeAntinodeOffsets(lines))
            {
                var antinodePosition = position + offset;

                for (var i = 0; antinodePosition.IsValidPosition(width: lines[0].Length, height: lines.Length); ++i)
                {
                    var index = antinodePosition.X * lines.Length + antinodePosition.Y;

                    harmonicAntinodes[index] = true;

                    if (i == 1)
                    {
                        strictAntinodes[index] = true;
                    }

                    antinodePosition += offset;
                }
            }

            var result1 = strictAntinodes.Count(true);
            var result2 = harmonicAntinodes.Count(true);

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static ReadOnlySpan<(Vector2D Position, Vector2D Offset)> ComputeAntinodeOffsets(ReadOnlySpan<string> lines)
        {
            var antennaGroups = new Dictionary<char, List<Vector2D>>();

            for (var y = 0; y < lines.Length; ++y)
            {
                for (var x = 0; x < lines[y].Length; ++x)
                {
                    var frequency = lines[y][x];

                    if (frequency == '.')
                    {
                        continue;
                    }

                    if (!antennaGroups.TryGetValue(frequency, out var antennas))
                    {
                        antennaGroups[frequency] = antennas = [];
                    }

                    antennas.Add(new Vector2D(x, y));
                }
            }

            var result = new List<(Vector2D Position, Vector2D Offset)>();

            foreach (var group in antennaGroups.Values)
            {
                for (var i = 0; i < group.Count; ++i)
                {
                    for (var j = 0; j < group.Count; ++j)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        result.Add((group[i], group[j] - group[i]));
                    }
                }
            }

            return CollectionsMarshal.AsSpan(result);
        }

        private readonly record struct Vector2D(int X, int Y)
        {
            public bool IsValidPosition(int width, int height) => 0 <= X && X < width && 0 <= Y && Y < height;

            public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);

            public static Vector2D operator -(Vector2D a, Vector2D b) => new(a.X - b.X, a.Y - b.Y);
        }
    }
}
