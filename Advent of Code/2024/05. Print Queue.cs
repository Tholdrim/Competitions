using System.Runtime.InteropServices;

namespace AdventOfCode.Year2024
{
    [TestClass]
    public class Day05
    {
        [TestMethod]
        [DataRow("Data/Sample 05.txt", 143, 123, DisplayName = "Sample")]
        [DataRow("Data/Input 05.secret", 3608, 4922, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2)
        {
            var lines = File.ReadAllLines(fileName);
            var pageOrderingRules = ParsePageOrderingRules(lines);

            var comparer = Comparer<int>.Create((a, b) => pageOrderingRules.Contains((b, a)) ? 1 : -1);

            var list = new List<int>();
            var (result1, result2) = (0, 0);

            foreach (ReadOnlySpan<char> line in lines[(pageOrderingRules.Count + 1)..])
            {
                foreach (var range in line.Split(','))
                {
                    list.Add(int.Parse(line[range]));
                }

                var update = CollectionsMarshal.AsSpan(list);
                var sortedUpdate = CloneAndSortUpdate(update, comparer);

                ref var result = ref update.SequenceEqual(sortedUpdate) ? ref result1 : ref result2;

                result += sortedUpdate[sortedUpdate.Length >> 1];

                list.Clear();
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static HashSet<(int, int)> ParsePageOrderingRules(ReadOnlySpan<string> lines)
        {
            var result = new HashSet<(int, int)>();
            Span<Range> ranges = stackalloc Range[2];

            foreach (ReadOnlySpan<char> line in lines)
            {
                if (line.Length == 0)
                {
                    break;
                }

                line.Split(ranges, '|');

                result.Add(new(int.Parse(line[ranges[0]]), int.Parse(line[ranges[1]])));
            }

            return result;
        }

        private static ReadOnlySpan<int> CloneAndSortUpdate(ReadOnlySpan<int> span, IComparer<int> comparer)
        {
            var result = span.ToArray();

            Array.Sort(result, comparer);

            return result;
        }
    }
}
