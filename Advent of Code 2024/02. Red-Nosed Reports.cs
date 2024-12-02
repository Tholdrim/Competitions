namespace AdventOfCode2024
{
    [TestClass]
    public class Day02
    {
        [TestMethod]
        [DataRow("Sample 02.txt", 2, 4, DisplayName = "Sample")]
        [DataRow("Input 02.txt", 526, 566, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2)
        {
            var (result1, result2) = (0, 0);

            foreach (var line in File.ReadLines(fileName))
            {
                var levels = line.Split(' ').Select(int.Parse).ToArray();
                var isReportSafe = IsReportSafe(levels);

                result1 += isReportSafe ? 1 : 0;
                result2 += isReportSafe || IsReportSafeWithProblemDampener(levels) ? 1 : 0;
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static bool IsReportSafeWithProblemDampener(int[] levels)
        {
            return Enumerable.Range(0, levels.Length)
                .Select<int, int[]>(i => [.. levels[..i], .. levels[(i + 1)..]])
                .Any(IsReportSafe);
        }

        private static bool IsReportSafe(int[] levels)
        {
            var delta = levels[1] - levels[0];

            if (delta == 0 || Math.Abs(delta) > 3)
            {
                return false;
            }

            return Enumerable.Range(1, levels.Length - 1)
                .Select(i => levels[i] - levels[i - 1])
                .All(d => d * delta > 0 && Math.Abs(d) <= 3);
        }
    }
}
