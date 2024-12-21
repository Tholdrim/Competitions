namespace AdventOfCode2024
{
    [TestClass]
    public class Day21
    {
        private static readonly Dictionary<char, Vector2D> NumericButtons = new()
        {
            ['7'] = new Vector2D(0, 0),
            ['4'] = new Vector2D(0, 1),
            ['1'] = new Vector2D(0, 2),
            ['X'] = new Vector2D(0, 3),
            ['8'] = new Vector2D(1, 0),
            ['5'] = new Vector2D(1, 1),
            ['2'] = new Vector2D(1, 2),
            ['0'] = new Vector2D(1, 3),
            ['9'] = new Vector2D(2, 0),
            ['6'] = new Vector2D(2, 1),
            ['3'] = new Vector2D(2, 2),
            ['A'] = new Vector2D(2, 3),
        };

        private static readonly Dictionary<char, Vector2D> DirectionalButtons = new()
        {
            ['X'] = new Vector2D(0, 0),
            ['<'] = new Vector2D(0, 1),
            ['^'] = new Vector2D(1, 0),
            ['v'] = new Vector2D(1, 1),
            ['A'] = new Vector2D(2, 0),
            ['>'] = new Vector2D(2, 1),
        };

        private static readonly Dictionary<(string, int), long> Cache = new();

        [TestMethod]
        [DataRow("Sample 21.txt", 126384L, 154115708116294L, DisplayName = "Sample")]
        [DataRow("Input 21.txt", 203814L, 248566068436630L, DisplayName = "Input")]
        public void Solve(string fileName, long expectedResult1, long expectedResult2)
        {
            var (result1, result2) = (0L, 0L);

            foreach (var code in File.ReadLines(fileName))
            {
                var numericPart = long.Parse(code.Where(char.IsDigit).ToArray());

                result1 += FindShortestKeypadLength(code, [NumericButtons, ..Enumerable.Repeat(DirectionalButtons, 2)]) * numericPart;
                Cache.Clear();
                result2 += FindShortestKeypadLength(code, [NumericButtons, ..Enumerable.Repeat(DirectionalButtons, 25)]) * numericPart;
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static List<string> GetMovements(Vector2D position, Vector2D targetPosition, Vector2D forbiddenPosition)
        {
            var result = new List<string>();

            var deltaX = Math.Abs(targetPosition.X - position.X);
            var newPositionX = new Vector2D(targetPosition.X, position.Y);

            if (deltaX > 0 && newPositionX != forbiddenPosition)
            {
                var direction = position.X < targetPosition.X ? '>' : '<';

                var prefix = new string(Enumerable.Repeat(direction, deltaX).ToArray());
                var suffixes = GetMovements(newPositionX, targetPosition, forbiddenPosition);

                result.AddRange(suffixes.Select(s => prefix + s));
            }

            var deltaY = Math.Abs(targetPosition.Y - position.Y);
            var newPositionY = new Vector2D(position.X, targetPosition.Y);

            if (deltaY > 0 && newPositionY != forbiddenPosition)
            {
                var direction = position.Y < targetPosition.Y ? 'v' : '^';

                var prefix = new string(Enumerable.Repeat(direction, deltaY).ToArray());
                var suffixes = GetMovements(newPositionY, targetPosition, forbiddenPosition);

                result.AddRange(suffixes.Select(s => prefix + s));
            }

            return result.Count > 0 ? result : ["A"];
        }

        private static long FindShortestKeypadLength(string sequence, Dictionary<char, Vector2D>[] keypads)
        {
            if (Cache.TryGetValue((sequence, keypads.Length), out var length))
            {
                return length;
            }

            var position = keypads[0]['A'];
            var forbiddenPosition = keypads[0]['X'];

            foreach (var button in sequence)
            {
                var targetPosition = keypads[0][button];
                var shortest = long.MaxValue;

                var movements = GetMovements(position, targetPosition, forbiddenPosition);
                foreach (var move in movements)
                {
                    if (keypads.Length == 1)
                    {
                        shortest = move.Length;
                        break;
                    }

                    var candidate = FindShortestKeypadLength(move, keypads[1..]);

                    if (candidate != -1 && (shortest == long.MaxValue || candidate < shortest))
                    {
                        shortest = candidate;
                    }
                }

                if (shortest == long.MaxValue)
                {
                    return Cache[(sequence, keypads.Length)] = -1L;
                }

                length += shortest;
                position = targetPosition;
            }

            return Cache[(sequence, keypads.Length)] = length;
        }

        internal record Vector2D(int X, int Y);
    }
}
