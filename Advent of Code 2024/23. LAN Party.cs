
namespace AdventOfCode2024
{
    [TestClass]
    public class Day23
    {
        [TestMethod]
        [DataRow("Sample 23.txt", 7, "co,de,ka,ta", DisplayName = "Sample")]
        [DataRow("Input 23.txt", 1368, "dd,ig,il,im,kb,kr,pe,ti,tv,vr,we,xu,zi", DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, string expectedResult2)
        {
            var connections = File.ReadLines(fileName)
                .Select(l => l.Split("-"))
                .SelectMany<string[], (string Computer1, string Computer2)>(d => [(d[0], d[1]), (d[1], d[0])])
                .ToLookup(p => p.Computer1, p => p.Computer2);

            var largestClique = new List<string>();

            BronKerbosch(connections, [], [.. connections.Select(g => g.Key)], [], largestClique);

            var result1 = FindTrianglesCount(connections);
            var result2 = string.Join(",", largestClique.Order());

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static void BronKerbosch(
            ILookup<string, string> connections,
            HashSet<string> potentialClique,
            HashSet<string> candidates,
            HashSet<string> excluded,
            List<string> largestClique)
        {
            if (candidates.Count == 0 && excluded.Count == 0)
            {
                if (potentialClique.Count > largestClique.Count)
                {
                    largestClique.Clear();
                    largestClique.AddRange(potentialClique);
                }

                return;
            }

            foreach (var candidate in candidates)
            {
                potentialClique.Add(candidate);

                BronKerbosch(
                    connections,
                    potentialClique,
                    [.. candidates.Intersect(connections[candidate])],
                    [.. excluded.Intersect(connections[candidate])],
                    largestClique);
                
                potentialClique.Remove(candidate);

                candidates.Remove(candidate);
                excluded.Add(candidate);
            }
        }

        private static int FindTrianglesCount(ILookup<string, string> connections)
        {
            var triangles = new HashSet<string>();

            foreach (var computer in connections.Select(g => g.Key).Where(c => c[0] == 't'))
            {
                foreach (var connectedComputer in connections[computer])
                {
                    foreach (var thirdComputer in connections[computer].Intersect(connections[connectedComputer]))
                    {
                        var identifier = string.Join(",", new[] { computer, connectedComputer, thirdComputer }.Order());

                        triangles.Add(identifier);
                    }
                }
            }

            return triangles.Count;
        }
    }
}
