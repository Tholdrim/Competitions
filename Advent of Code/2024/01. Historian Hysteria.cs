namespace AdventOfCode.Year2024
{
    [TestClass]
    public class Day01
    {
        [TestMethod]
        [DataRow("Data/Sample 01.txt", 11, 31, DisplayName = "Sample")]
        [DataRow("Data/Input 01.secret", 1197984, 23387399, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2)
        {
            var leftList = new List<int>();
            var rightList = new List<int>();
            var rightListElementCounts = new Dictionary<int, int>();

            ParseInput();

            leftList.Sort();
            rightList.Sort();

            int leftListElement;
            var (result1, result2) = (0, 0);

            for (var i = 0; i < leftList.Count; ++i)
            {
                result1 += Math.Abs((leftListElement = leftList[i]) - rightList[i]);
                result2 += leftListElement * rightListElementCounts.GetValueOrDefault(leftListElement);
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);

            void ParseInput()
            {
                int rightListElement;
                Span<Range> ranges = stackalloc Range[2];

                foreach (ReadOnlySpan<char> line in File.ReadLines(fileName))
                {
                    line.Split(ranges, "   ");

                    leftList.Add(int.Parse(line[ranges[0]]));
                    rightList.Add(rightListElement = int.Parse(line[ranges[1]]));

                    rightListElementCounts[rightListElement] = rightListElementCounts.GetValueOrDefault(rightListElement) + 1;
                }
            }
        }
    }
}
