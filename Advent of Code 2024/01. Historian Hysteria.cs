namespace AdventOfCode2024
{
    [TestClass]
    public class Day01
    {
        [TestMethod]
        [DataRow("Sample 01.txt", 11, 31, DisplayName = "Sample")]
        [DataRow("Input 01.txt", 1197984, 23387399, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2)
        {
            var leftList = new List<int>();
            var rightList = new List<int>();
            var rightListElementCounts = new Dictionary<int, int>();

            foreach (var line in File.ReadLines(fileName))
            {
                var data = line.Split("   ");
                var rightListElement = int.Parse(data[1]);

                leftList.Add(int.Parse(data[0]));
                rightList.Add(rightListElement);

                rightListElementCounts[rightListElement] = rightListElementCounts.GetValueOrDefault(rightListElement) + 1;
            }

            leftList.Sort();
            rightList.Sort();

            var result1 = leftList.Zip(rightList, (l, r) => Math.Abs(l - r)).Sum();
            var result2 = leftList.Sum(e => e * rightListElementCounts.GetValueOrDefault(e));

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }
    }
}
