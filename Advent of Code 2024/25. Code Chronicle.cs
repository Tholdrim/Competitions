namespace AdventOfCode2024
{
    [TestClass]
    public class Day25
    {
        [TestMethod]
        [DataRow("Sample 25.txt", 3, DisplayName = "Sample")]
        [DataRow("Input 25.txt", 3065, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult)
        {
            var lines = File.ReadAllLines(fileName);

            var width = lines[0].Length;
            var height = lines.TakeWhile(l => l.Length > 0).Count();

            var locks = new List<int[]>();
            var keys = new List<int[]>();

            for (var i = 0; i < lines.Length; i += height + 1)
            {
                var heights = new int[width];
                var schematic = lines[i..(i + height)];
                var collection = schematic[0][0] == '#' ? locks : keys;

                for (var x = 0; x < width; ++x)
                {
                    heights[x] = Enumerable.Range(0, height).Count(y => schematic[y][x] == '#');
                }

                collection.Add(heights);
            }

            var result = keys.Sum(k => locks.Count(l => Enumerable.Range(0, width).All(i => k[i] + l[i] <= height)));

            Assert.AreEqual(expectedResult, result);
        }
    }
}
