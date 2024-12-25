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
            var height = lines.TakeWhile(l => l.Length > 0).Count();
            var width = lines[0].Length;

            var locks = new List<int[]>();
            var keys = new List<int[]>();

            for (var i = 0; i < lines.Length; i += height + 1)
            {
                var schematic = lines[i..(i + height)];

                if (schematic[0].Contains('.'))
                {
                    var key = new int[width];

                    for (var j = 0; j < width; ++j)
                    {
                        key[j] = height - 2 - Enumerable.Range(1, height - 1).TakeWhile(y => schematic[y][j] == '.').Count();
                    }

                    keys.Add(key);
                }
                else
                {
                    var @lock = new int[width];

                    for (var j = 0; j < width; ++j)
                    {
                        @lock[j] = Enumerable.Range(1, height - 1).TakeWhile(y => schematic[y][j] == '#').Count();
                    }

                    locks.Add(@lock) ;
                }
            }

            var result = 0;

            for (var i = 0; i < locks.Count; ++i)
            {
                for (var j = 0; j < keys.Count; ++j)
                {
                    if (Enumerable.Range(0, width).All(k => locks[i][k] + keys[j][k] <= height - 2))
                    {
                        ++result;
                    }
                }
            }

            Assert.AreEqual(expectedResult, result);
        }
    }
}
