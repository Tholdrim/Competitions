using System.Runtime.InteropServices;

namespace AdventOfCode.Year2024
{
    [TestClass]
    public class Day02
    {
        [TestMethod]
        [DataRow("Data/Sample 02.txt", 2, 4, DisplayName = "Sample")]
        [DataRow("Data/Input 02.secret", 526, 566, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2)
        {
            var list = new List<int>();
            var (result1, result2) = (0, 0);

            foreach (ReadOnlySpan<char> line in File.ReadLines(fileName))
            {
                foreach (var range in line.Split(' '))
                {
                    list.Add(int.Parse(line[range]));
                }

                var levels = CollectionsMarshal.AsSpan(list);
                var isReportSafe = IsReportSafe(levels);

                result1 += isReportSafe ? 1 : 0;
                result2 += isReportSafe || IsReportSafeWithProblemDampener(levels) ? 1 : 0;

                list.Clear();
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static bool IsReportSafeWithProblemDampener(ReadOnlySpan<int> levels)
        {
            for (var i = 0; i < levels.Length; ++i)
            {
                if (IsReportSafe([.. levels[..i], .. levels[(i + 1)..]]))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsReportSafe(ReadOnlySpan<int> levels)
        {
            var firstDelta = levels[1] - levels[0];

            if (firstDelta == 0 || Math.Abs(firstDelta) > 3)
            {
                return false;
            }

            for (var i = 2; i < levels.Length; ++i)
            {
                var delta = levels[i] - levels[i - 1];

                if (delta * firstDelta <= 0 || Math.Abs(delta) > 3)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
