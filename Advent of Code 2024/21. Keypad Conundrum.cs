using Keypad = System.Collections.Generic.Dictionary<char, AdventOfCode2024.Day21.Vector2D>;

namespace AdventOfCode2024
{
    [TestClass]
    public class Day21
    {
        private static readonly Keypad NumericButtons = new()
        {
            ['7'] = new Vector2D(0, 0), ['8'] = new Vector2D(1, 0), ['9'] = new Vector2D(2, 0),
            ['4'] = new Vector2D(0, 1), ['5'] = new Vector2D(1, 1), ['6'] = new Vector2D(2, 1),
            ['1'] = new Vector2D(0, 2), ['2'] = new Vector2D(1, 2), ['3'] = new Vector2D(2, 2),
            ['X'] = new Vector2D(0, 3), ['0'] = new Vector2D(1, 3), ['A'] = new Vector2D(2, 3)
        };

        private static readonly Keypad DirectionalButtons = new()
        {
            ['X'] = new Vector2D(0, 0), ['^'] = new Vector2D(1, 0), ['A'] = new Vector2D(2, 0),
            ['<'] = new Vector2D(0, 1), ['v'] = new Vector2D(1, 1), ['>'] = new Vector2D(2, 1)
        };

        private static readonly Keypad[] KeypadsForFirstHistorian = [NumericButtons, ..Enumerable.Repeat(DirectionalButtons, 2)];

        private static readonly Keypad[] KeypadsForSecondHistorian = [NumericButtons, ..Enumerable.Repeat(DirectionalButtons, 25)];

        [TestMethod]
        [DataRow("Sample 21.txt", 126384L, 154115708116294L, DisplayName = "Sample")]
        [DataRow("Input 21.txt", 203814L, 248566068436630L, DisplayName = "Input")]
        public void Solve(string fileName, long expectedResult1, long expectedResult2)
        {
            var (result1, result2) = (0L, 0L);

            foreach (var code in File.ReadLines(fileName))
            {
                var numericPart = long.Parse(code.Where(char.IsDigit).ToArray());

                result1 += FindShortestSequenceLength(code, KeypadsForFirstHistorian) * numericPart;
                result2 += FindShortestSequenceLength(code, KeypadsForSecondHistorian) * numericPart;
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static long FindShortestSequenceLength(string sequence, Keypad[] keypads) => FindShortestSequenceLength(sequence, keypads, []);

        private static long FindShortestSequenceLength(string sequence, Keypad[] keypads, Dictionary<(string, int), long> cache)
        {
            if (cache.TryGetValue((sequence, keypads.Length), out var length))
            {
                return length;
            }

            var position = keypads[0]['A'];
            var forbiddenPosition = keypads[0]['X'];

            foreach (var button in sequence)
            {
                var shortestLength = long.MaxValue;
                var targetPosition = keypads[0][button];

                foreach (var moveSequence in GetMoveSequences(position, targetPosition, forbiddenPosition))
                {
                    if (keypads.Length == 1)
                    {
                        shortestLength = moveSequence.Length;

                        break;
                    }

                    shortestLength = Math.Min(shortestLength, FindShortestSequenceLength(moveSequence, keypads[1..], cache));
                }

                length += shortestLength;
                position = targetPosition;
            }

            return cache[(sequence, keypads.Length)] = length;
        }

        private static List<string> GetMoveSequences(Vector2D position, Vector2D targetPosition, Vector2D forbiddenPosition)
        {
            var result = new List<string>();

            var nextHorizontalPosition = new Vector2D(targetPosition.X, position.Y);
            var nextVerticalPosition = new Vector2D(position.X, targetPosition.Y);

            if (position != nextHorizontalPosition && nextHorizontalPosition != forbiddenPosition)
            {
                var prefix = new string(position.X < targetPosition.X ? '>' : '<', Math.Abs(targetPosition.X - position.X));
                var suffixes = GetMoveSequences(nextHorizontalPosition, targetPosition, forbiddenPosition);

                result.AddRange(suffixes.Select(s => prefix + s));
            }

            if (position != nextVerticalPosition && nextVerticalPosition != forbiddenPosition)
            {
                var prefix = new string(position.Y < targetPosition.Y ? 'v' : '^', Math.Abs(targetPosition.Y - position.Y));
                var suffixes = GetMoveSequences(nextVerticalPosition, targetPosition, forbiddenPosition);

                result.AddRange(suffixes.Select(s => prefix + s));
            }

            return result.Count > 0 ? result : ["A"];
        }

        internal record Vector2D(int X, int Y);
    }
}
