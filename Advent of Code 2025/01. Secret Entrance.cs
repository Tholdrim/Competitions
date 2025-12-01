namespace AdventOfCode2025
{
    [TestClass]
    public class Day01
    {
        [TestMethod]
        [DataRow("Sample 01.txt", 3, 6, DisplayName = "Sample")]
        [DataRow("Input 01.txt", 1092, 6616, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2)
        {
            var (result1, result2) = (0, 0);
            var position = 50;

            foreach (var line in File.ReadLines(fileName))
            {
                var distance = int.Parse(line[1..]);

                if (line[0] == 'L')
                {
                    distance = -distance;
                    result2 += (100 - position - distance) / 100 - (100 - position) / 100;
                }
                else
                {
                    result2 += (position + distance) / 100;
                }

                position = (position + distance) % 100;

                if (position < 0)
                {
                    position += 100;
                }

                if (position == 0)
                {
                    ++result1;
                }
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }
    }
}
