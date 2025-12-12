namespace AdventOfCode2025
{
    [TestClass]
    public class Day12
    {
        [TestMethod]
        //[DataRow("Sample 12.txt", 2, DisplayName = "Sample")]
        [DataRow("Input 12.txt", 410, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult)
        {
            var result = 0;

            foreach (var line in File.ReadAllLines(fileName).Skip(30))
            {
                var data = line.Split([' ', 'x', ':'], StringSplitOptions.RemoveEmptyEntries);
                var area = int.Parse(data[0]) * int.Parse(data[1]);
                var needed = data[2..].Select(int.Parse).Sum() * 9;

                if (needed <= area)
                {
                    ++result;
                }
            }

            Assert.AreEqual(expectedResult, result);
        }
    }
}
