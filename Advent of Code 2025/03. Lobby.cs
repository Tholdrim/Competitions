namespace AdventOfCode2025
{
    [TestClass]
    public class Day03
    {
        [TestMethod]
        [DataRow("Sample 03.txt", 357L, 3121910778619L, DisplayName = "Sample")]
        [DataRow("Input 03.txt", 17403L, 173416889848394L, DisplayName = "Input")]
        public void Solve(string fileName, long expectedResult1, long expectedResult2)
        {
            var (result1, result2) = (0L, 0L);

            foreach (ReadOnlySpan<char> line in File.ReadLines(fileName))
            {
                result1 += GetJoltage(line, 2);
                result2 += GetJoltage(line, 12);
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        public static long GetJoltage(ReadOnlySpan<char> sequence, int batteries)
        {
            var joltage = 0L;
            var lastIndex = -1;

            for (var i = batteries - 1; i >= 0; --i)
            {
                var index = FindHighestCharIndex(sequence[(lastIndex + 1)..^i]);

                joltage = joltage * 10 + (sequence[lastIndex + index + 1] - '0');
                lastIndex += index + 1;
            }

            return joltage;
        }

        public static int FindHighestCharIndex(ReadOnlySpan<char> sequence)
        {
            var index = 0;

            for (var i = 1; i < sequence.Length; ++i)
            {
                if (sequence[i] > sequence[index])
                {
                    index = i;
                }
            }

            return index;
        }
    }
}
