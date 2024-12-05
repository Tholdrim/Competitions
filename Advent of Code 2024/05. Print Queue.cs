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
            var (result1, result2) = (0, 0);

            var lines = File.ReadAllLines(fileName);

            var pageOrderingRules = lines
                .TakeWhile(l => l.Length > 0)
                .Select(l => l.Split('|'))
                .ToLookup(d => int.Parse(d[0]), d => int.Parse(d[1]));

            var updates = lines
                .Skip(pageOrderingRules.Sum(g => g.Count()) + 1)
                .Select(l => l.Split(',').Select(int.Parse).ToList());

            foreach (var update in updates)
            {
                if (IsUpdateInCorrectOrder(update, pageOrderingRules))
                {
                    result1 += update[update.Count / 2];

                    continue;
                }

                var sortedUpdate = SortUpdate(update, pageOrderingRules);

                result2 += sortedUpdate[sortedUpdate.Count / 2];
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static bool IsUpdateInCorrectOrder(List<int> update, ILookup<int, int> pageOrderingRules)
        {
            return Enumerable.Range(1, update.Count - 1)
                .All(i => Enumerable.Range(0, i)
                    .All(j => !pageOrderingRules[update[i]].Contains(update[j])));
        }

        private static List<int> SortUpdate(List<int> update, ILookup<int, int> pageOrderingRules)
        {
            var result = new List<int>(update.Count);

            update.ForEach(p => result.Insert(pageOrderingRules[p].DefaultIfEmpty().Max(result.IndexOf) + 1, p));

            return result;
        }
    }
}
