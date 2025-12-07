namespace AdventOfCode2025
{
    [TestClass]
    public class Day07
    {
        [TestMethod]
        [DataRow("Sample 07.txt", 21, 40L, DisplayName = "Sample")]
        [DataRow("Input 07.txt", 1635, 58097428661390L, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, long expectedResult2)
        {
            var grid = File.ReadLines(fileName).ToArray();

            var result1 = 0;
            var startingX = grid[0].IndexOf('S');
            var beams = new Dictionary<int, long> { [startingX] = 1L };

            foreach (var row in grid[1..])
            {
                for (var i = 0; i < row.Length; ++i)
                {
                    if (row[i] == '^' && beams.Remove(i, out var value))
                    {
                        ++result1;
                        beams[i - 1] = beams.GetValueOrDefault(i - 1) + value;
                        beams[i + 1] = beams.GetValueOrDefault(i + 1) + value;
                    }
                }
            }

            var result2 = beams.Values.Sum();

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }
    }
}
