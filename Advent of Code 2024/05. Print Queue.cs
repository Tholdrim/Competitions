using System.Collections.Frozen;

namespace AdventOfCode2024
{
    [TestClass]
    public class Day05
    {
        [TestMethod]
        [DataRow("Sample 05.txt", 143, 123, DisplayName = "Sample")]
        [DataRow("Input 05.txt", 3608, 4922, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2)
        {
            var lines = File.ReadAllLines(fileName);

            var pageOrderingRules = lines
                .TakeWhile(l => l.Length > 0)
                .Select(l => l.Split('|'))
                .Select(d => (int.Parse(d[0]), int.Parse(d[1])))
                .ToFrozenSet();

            var updates = lines[(pageOrderingRules.Count + 1)..]
                .Select(l => l.Split(',').Select(int.Parse).ToArray());

            var (result1, result2) = (0, 0);
            var comparer = Comparer<int>.Create((a, b) => pageOrderingRules.Contains((b, a)) ? 1 : -1);

            foreach (var update in updates)
            {
                var sortedUpdate = CloneAndSort(update, comparer);
                ref var result = ref update.SequenceEqual(sortedUpdate) ? ref result1 : ref result2;

                result += sortedUpdate[sortedUpdate.Length / 2];
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static int[] CloneAndSort(ReadOnlySpan<int> array, IComparer<int> comparer)
        {
            var result = array.ToArray();

            Array.Sort(result, comparer);

            return result;
        }
    }
}
