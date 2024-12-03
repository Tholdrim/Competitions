using System.Text.RegularExpressions;

namespace AdventOfCode.Year2024
{
    [TestClass]
    public partial class Day03
    {
        [GeneratedRegex(@"do(?:n't)?\(\)|mul\((\d{1,3}),(\d{1,3})\)")]
        private static partial Regex Regex { get; }

        [TestMethod]
        [DataRow("Data/Sample 03.txt", 161, 48, DisplayName = "Sample")]
        [DataRow("Data/Input 03.secret", 156388521, 75920122, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2)
        {
            var allowMultiply = true;
            var (result1, result2) = (0, 0);

            var input = File.ReadAllText(fileName);

            foreach (Match match in Regex.Matches(input))
            {
                if (!match.Groups[1].Success && !match.Groups[2].Success)
                {
                    allowMultiply = match.ValueSpan.SequenceEqual("do()");

                    continue;
                }

                var product = int.Parse(match.Groups[1].ValueSpan) * int.Parse(match.Groups[2].ValueSpan);

                result1 += product;
                result2 += allowMultiply ? product : 0;
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }
    }
}
