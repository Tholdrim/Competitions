namespace AdventOfCode2025
{
    [TestClass]
    public class Day08
    {
        [TestMethod]
        [DataRow("Sample 08.txt", 40, 25272, 10, DisplayName = "Sample")]
        [DataRow("Input 08.txt", 54180, 0, 1000, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2, int iterations)
        {
            var positions = new List<(int X, int Y, int Z)>();

            foreach (var line in File.ReadLines(fileName))
            {
                positions.Add(ParseCoordinates(line));
            }

            var pairs = new List<(long Distance, int Index1, int Index2)>();

            for (var i = 0; i < positions.Count - 1; ++i)
            {
                var (minDistance, index) = (long.MaxValue, -1);

                for (var j = i + 1; j < positions.Count; ++j)
                {
                    var distance = CalculateDistance(positions[i], positions[j]);

                    pairs.Add((distance, i, j));
                }
            }

            var result1 = CalculatePart1(positions.Count, iterations, pairs);
            var result2 = CalculatePart2(positions, pairs);

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static int CalculatePart1(int length, int iterations, List<(long Distance, int Index1, int Index2)> pairs)
        {
            var dsu = new DisjointSetUnion(length);

            foreach (var pair in pairs.OrderBy(p => p.Distance).Take(iterations))
            {
                var r1 = dsu.Find(pair.Index1);
                var r2 = dsu.Find(pair.Index2);

                if (r1 != r2)
                {
                    dsu.Union(r1, r2);
                }
            }

            var circuitSizes = new List<int>(length);

            for (var i = 0; i < length; ++i)
            {
                if (dsu.Find(i) == i)
                {
                    circuitSizes.Add(dsu.Size(i));
                }
            }

            return circuitSizes.OrderByDescending(s => s).Take(3).Aggregate(1, (a, b) => a * b);
        }

        private static int CalculatePart2(List<(int X, int Y, int Z)> positions, List<(long Distance, int Index1, int Index2)> pairs)
        {
            var dsu = new DisjointSetUnion(positions.Count);
            var circuits = positions.Count;

            foreach (var pair in pairs.OrderBy(p => p.Distance))
            {
                var r1 = dsu.Find(pair.Index1);
                var r2 = dsu.Find(pair.Index2);

                if (r1 != r2)
                {
                    dsu.Union(r1, r2);
                    --circuits;

                    if (circuits == 1)
                    {
                        return positions[pair.Index1].X * positions[pair.Index2].X;
                    }
                }
            }

            throw new NotImplementedException();
        }

        private static long CalculateDistance((int X, int Y, int Z) first, (int X, int Y, int Z) second)
        {
            var deltaX = (long)(first.X - second.X);
            var deltaY = (long)(first.Y - second.Y);
            var deltaZ = (long)(first.Z - second.Z);

            return deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ;
        }

        private static (int, int, int) ParseCoordinates(string line)
        {
            var data = line.Split(',');

            return (int.Parse(data[0]), int.Parse(data[1]), int.Parse(data[2]));
        }

        private class DisjointSetUnion
        {
            private int[] _parent;
            private int[] _size;

            public DisjointSetUnion(int length)
            {
                _parent = new int[length];
                _size = new int[length];

                for (var i = 0; i < length; ++i)
                {
                    _parent[i] = i;
                    _size[i] = 1;
                }
            }

            public int Find(int x)
            {
                while (_parent[x] != x)
                {
                    _parent[x] = _parent[_parent[x]];
                    x = _parent[x];
                }

                return x;
            }

            public int Size(int x) => _size[x];

            public void Union(int a, int b)
            {
                a = Find(a);
                b = Find(b);

                if (a == b)
                {
                    return;
                }

                if (_size[a] < _size[b])
                {
                    (a, b) = (b, a);
                }

                _parent[b] = a;
                _size[a] += _size[b];
            }
        }
    }
}
