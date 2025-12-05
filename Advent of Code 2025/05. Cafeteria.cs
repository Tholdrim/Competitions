namespace AdventOfCode2025
{
    [TestClass]
    public class Day05
    {
        [TestMethod]
        [DataRow("Sample 05.txt", 3, 14L, DisplayName = "Sample")]
        [DataRow("Input 05.txt", 828, 352681648086146L, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, long expectedResult2)
        {
            var (result1, result2) = (0, 0L);
            var freshRanges = new List<(long, long)>();
            var lines = File.ReadAllLines(fileName);

            long lineIndex;

            for (lineIndex = 0; lines[lineIndex].Length > 0; ++lineIndex)
            {
                var data = lines[lineIndex].Split('-');

                freshRanges.Add((long.Parse(data[0]), long.Parse(data[1])));
            }

            var mergedFreshRanges = MergeRanges(freshRanges);

            foreach (var (start, end) in mergedFreshRanges)
            {
                result2 += (end - start + 1);
            }

            for (lineIndex = lineIndex + 1; lineIndex < lines.Length; ++lineIndex)
            {
                var id = long.Parse(lines[lineIndex]);

                if (IsInAnyRange(mergedFreshRanges, id))
                {
                    ++result1;
                }
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static List<(long start, long end)> MergeRanges(List<(long, long)> ranges)
        {
            ranges.Sort((range1, range2) =>
            {
                var startComparison = range1.Item1.CompareTo(range2.Item1);

                return startComparison != 0 ? startComparison : range1.Item2.CompareTo(range2.Item2);
            });

            var mergedRanges = new List<(long start, long end)>();
            var (currentStart, currentEnd) = ranges[0];

            for (var i = 1; i < ranges.Count; i++)
            {
                var (start, end) = ranges[i];

                if (start <= currentEnd + 1)
                {
                    if (end > currentEnd)
                    {
                        currentEnd = end;
                    }
                }
                else
                {
                    mergedRanges.Add((currentStart, currentEnd));
                    (currentStart, currentEnd) = (start, end);
                }
            }

            mergedRanges.Add((currentStart, currentEnd));

            return mergedRanges;
        }

        private static bool IsInAnyRange(List<(long, long)> mergedRanges, long id)
        {
            var (lowerBound, upperBound) = (0, mergedRanges.Count - 1);

            while (lowerBound <= upperBound)
            {
                var middleIndex = lowerBound + ((upperBound - lowerBound) >> 1);
                var (rangeStart, rangeEnd) = mergedRanges[middleIndex];

                if (id < rangeStart)
                {
                    upperBound = middleIndex - 1;
                }
                else if (id > rangeEnd)
                {
                    lowerBound = middleIndex + 1;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
    }
}
