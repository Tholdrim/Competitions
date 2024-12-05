using System.Text.RegularExpressions;

namespace AdventOfCode2024
{
    [TestClass]
    public partial class Day03
    {
        [GeneratedRegex(@"mul\((?<x>\d{1,3}),(?<y>\d{1,3})\)|(?<instruction>do\(\)|don't\(\))")]
        private static partial Regex Regex { get; }

        [TestMethod]
        [DataRow("Sample 03.txt", 161, 48, DisplayName = "Sample")]
        [DataRow("Input 03.txt", 156388521, 75920122, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2)
        {
            var allowMultiply = true;
            var (result1, result2) = (0, 0);

            var input = File.ReadAllText(fileName);

            foreach (var match in Regex.Matches(input).OfType<Match>())
            {
                if (match.Groups["instruction"].Success)
                {
                    allowMultiply = match.Groups["instruction"].Value == "do()";

                    continue;
                }

                var product = int.Parse(match.Groups["x"].Value) * int.Parse(match.Groups["y"].Value);

                result1 += product;
                result2 += allowMultiply ? product : 0;
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }
    }
}
