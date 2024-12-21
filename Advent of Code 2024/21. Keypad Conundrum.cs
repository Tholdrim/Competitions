using Keypad = System.Collections.Generic.Dictionary<char, AdventOfCode2024.Day21.Vector2D>;

namespace AdventOfCode2024
{
    [TestClass]
    public class Day21
    {
        private static readonly Keypad DirectionalButtons = new()
        {
            ['X'] = new Vector2D(0, 0), ['^'] = new Vector2D(1, 0), ['A'] = new Vector2D(2, 0),
            ['<'] = new Vector2D(0, 1), ['v'] = new Vector2D(1, 1), ['>'] = new Vector2D(2, 1)
        };

        private static readonly Keypad NumericButtons = new()
        {
            ['7'] = new Vector2D(0, 0), ['8'] = new Vector2D(1, 0), ['9'] = new Vector2D(2, 0),
            ['4'] = new Vector2D(0, 1), ['5'] = new Vector2D(1, 1), ['6'] = new Vector2D(2, 1),
            ['1'] = new Vector2D(0, 2), ['2'] = new Vector2D(1, 2), ['3'] = new Vector2D(2, 2),
            ['X'] = new Vector2D(0, 3), ['0'] = new Vector2D(1, 3), ['A'] = new Vector2D(2, 3)
        };

        private static readonly Keypad[] FirstHistorianKeypads = [NumericButtons, ..Enumerable.Repeat(DirectionalButtons, 2)];

        private static readonly Keypad[] SecondHistorianKeypads = [NumericButtons, ..Enumerable.Repeat(DirectionalButtons, 25)];

        [TestMethod]
        [DataRow("Sample 21.txt", 126384L, 154115708116294L, DisplayName = "Sample")]
        [DataRow("Input 21.txt", 203814L, 248566068436630L, DisplayName = "Input")]
        public void Solve(string fileName, long expectedResult1, long expectedResult2)
        {
            var (result1, result2) = (0L, 0L);

            foreach (var code in File.ReadLines(fileName))
            {
                var numericPart = long.Parse(code.AsSpan()[..3]);

                result1 += FindShortestSequenceLength(code, FirstHistorianKeypads, []) * numericPart;
                result2 += FindShortestSequenceLength(code, SecondHistorianKeypads, []) * numericPart;
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static long FindShortestSequenceLength(string sequence, ReadOnlySpan<Keypad> keypads, Dictionary<(string, int), long> cache)
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

        private static IEnumerable<string> GetMoveSequences(Vector2D position, Vector2D targetPosition, Vector2D forbiddenPosition)
        {
            var nextHorizontalPosition = new Vector2D(targetPosition.X, position.Y);
            var nextVerticalPosition = new Vector2D(position.X, targetPosition.Y);

            if (nextHorizontalPosition == nextVerticalPosition)
            {
                yield return "A";
                yield break;
            }

            if (position != nextHorizontalPosition && nextHorizontalPosition != forbiddenPosition)
            {
                var prefix = new string(position.X < targetPosition.X ? '>' : '<', Math.Abs(targetPosition.X - position.X));
                var suffix = GetMoveSequences(nextHorizontalPosition, targetPosition, forbiddenPosition).Single();

                yield return prefix + suffix;
            }

            if (position != nextVerticalPosition && nextVerticalPosition != forbiddenPosition)
            {
                var prefix = new string(position.Y < targetPosition.Y ? 'v' : '^', Math.Abs(targetPosition.Y - position.Y));
                var suffix = GetMoveSequences(nextVerticalPosition, targetPosition, forbiddenPosition).Single();

                yield return prefix + suffix;
            }
        }

        internal record Vector2D(int X, int Y);
    }
}
