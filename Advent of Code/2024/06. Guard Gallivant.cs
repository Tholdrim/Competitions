using System.Collections.Specialized;

namespace AdventOfCode.Year2024
{
    [TestClass]
    public class Day06
    {
        [TestMethod]
        [DataRow("Data/Sample 06.txt", 41, 6, DisplayName = "Sample")]
        [DataRow("Data/Input 06.secret", 4752, 1719, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2)
        {
            var map = File.ReadAllLines(fileName).Select(l => "\0" + l + "\0").ToArray();
            var borderRow = new string('\0', map[0].Length);

            map = [borderRow, .. map, borderRow];

            var startingPosition = LocateFirstOccurrence(map, '^');
            var (result1, result2) = PerformGuardPatrol(map, startingPosition);

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static (int, int) PerformGuardPatrol(string[] map, Vector2D position)
        {
            var direction = new Vector2D(0, -1);
            Span<bool> visitedPositions = stackalloc bool[map.Length * map[0].Length];
            var obstructionCandidates = new List<(Vector2D Position, Vector2D Direction, Vector2D ObstructionPosition)>();

            visitedPositions.Clear();

            while (map[position.Y][position.X] != '\0')
            {
                var nextPosition = position + direction;

                if (map[nextPosition.Y][nextPosition.X] == '#')
                {
                    direction = direction.RotateVector();

                    continue;
                }

                visitedPositions[position.X * map.Length + position.Y] = true;

                if (!visitedPositions[nextPosition.X * map.Length + nextPosition.Y])
                {
                    obstructionCandidates.Add(new(position, direction.RotateVector(), nextPosition));
                }

                position = nextPosition;
            }

            var obstructions = obstructionCandidates.AsParallel().Count(c => IsLooped(map, c.Position, c.Direction, c.ObstructionPosition));

            return (visitedPositions.Count(true), obstructions);
        }

        private static bool IsLooped(string[] map, Vector2D position, Vector2D direction, Vector2D obstructionPosition)
        {
            var mapSize = map.Length * map[0].Length;
            Span<BitVector32> guardMovements = stackalloc BitVector32[mapSize >> 3];

            guardMovements.Clear();

            while (map[position.Y][position.X] != '\0' && TryToMarkMovement(guardMovements))
            {
                var nextPosition = position + direction;

                if (map[nextPosition.Y][nextPosition.X] == '#' || nextPosition == obstructionPosition)
                {
                    direction = direction.RotateVector();

                    continue;
                }

                position = nextPosition;
            }

            return map[position.Y][position.X] != '\0';

            bool TryToMarkMovement(Span<BitVector32> span)
            {
                var directionIndex = (4 + 3 * direction.Y + direction.X) >> 1;
                var index = directionIndex * mapSize + map.Length * position.X + position.Y;

                return span[index >> 5][1 << (index & 31)] ^= true;
            }
        }

        private static Vector2D LocateFirstOccurrence(ReadOnlySpan<string> map, char element)
        {
            for (var y = 1; y < map.Length - 1; ++y)
            {
                for (var x = 1; x < map[y].Length - 1; ++x)
                {
                    if (map[y][x] == element)
                    {
                        return new Vector2D(x, y);
                    }
                }
            }

            throw new InvalidOperationException();
        }

        private readonly record struct Vector2D(int X, int Y)
        {
            public Vector2D RotateVector() => new(-Y, X);

            public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);
        }
    }
}
