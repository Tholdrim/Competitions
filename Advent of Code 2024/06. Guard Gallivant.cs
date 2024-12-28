namespace AdventOfCode2024
{
    [TestClass]
    public class Day06
    {
        [TestMethod]
        [DataRow("Sample 06.txt", 41, 6, DisplayName = "Sample")]
        [DataRow("Input 06.txt", 4752, 1719, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2)
        {
            var map = File.ReadAllLines(fileName).Select(l => "*" + l + "*").ToArray();
            var borderRow = new string('*', map[0].Length);

            map = [borderRow, .. map, borderRow];

            var startingPosition = FindPosition(map, '^');
            var (result1, result2) = PerformGuardPatrol(map, startingPosition);

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static (int, int) PerformGuardPatrol(string[] map, Vector2D position)
        {
            var direction = new Vector2D(0, -1);
            var visitedPositions = new HashSet<Vector2D>();
            var obstructionCandidates = new List<(Vector2D Position, Vector2D Direction, Vector2D ObstructionPosition)>();

            while (map[position.Y][position.X] != '*')
            {
                var nextPosition = position + direction;

                if (map[nextPosition.Y][nextPosition.X] == '#')
                {
                    direction = direction.RotateVector();

                    continue;
                }

                visitedPositions.Add(position);

                if (!visitedPositions.Contains(nextPosition))
                {
                    obstructionCandidates.Add((position, direction.RotateVector(), nextPosition));
                }

                position = nextPosition;
            }

            var obstructions = obstructionCandidates.AsParallel().Count(x => IsLooped(map, x.Position, x.Direction, x.ObstructionPosition));

            return (visitedPositions.Count, obstructions);
        }

        private static bool IsLooped(string[] map, Vector2D position, Vector2D direction, Vector2D obstructionPosition)
        {
            var mapSize = map.Length * map[0].Length;
            var guardMovements = new bool[mapSize * 4];

            while (map[position.Y][position.X] != '*' && (guardMovements[GetCurrentIndex()] ^= true))
            {
                var nextPosition = position + direction;

                if (map[nextPosition.Y][nextPosition.X] == '#' || nextPosition == obstructionPosition)
                {
                    direction = direction.RotateVector();

                    continue;
                }

                position = nextPosition;
            }

            return map[position.Y][position.X] != '*';

            int GetCurrentIndex()
            {
                var directionIndex = (4 + 3 * direction.Y + direction.X) / 2;

                return directionIndex * mapSize + map.Length * position.X + position.Y;
            }
        }

        private static Vector2D FindPosition(string[] map, char element)
        {
            return Enumerable.Range(1, map.Length - 2)
                .SelectMany(y => Enumerable.Range(1, map[y].Length - 2), (y, x) => new Vector2D(x, y))
                .First(p => map[p.Y][p.X] == element);
        }

        private readonly record struct Vector2D(int X, int Y)
        {
            public Vector2D RotateVector() => new(-Y, X);

            public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);
        }
    }
}
