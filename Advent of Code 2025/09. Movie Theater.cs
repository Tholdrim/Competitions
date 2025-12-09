namespace AdventOfCode2025
{
    [TestClass]
    public class Day09
    {
        [TestMethod]
        [DataRow("Sample 09.txt", 50L, 24L, DisplayName = "Sample")]
        [DataRow("Input 09.txt", 4755278336L, 1534043700L, DisplayName = "Input")]
        public void Solve(string fileName, long expectedResult1, long expectedResult2)
        {
            var (result1, result2) = (0L, 0L);
            var positions = new List<(int X, int Y)>();

            foreach (var line in File.ReadLines(fileName))
            {
                positions.Add(ParseCoordinates(line));
            }

            for (var i = 0; i < positions.Count; ++i)
            {
                for (var j = i + 1; j < positions.Count; ++j)
                {
                    var deltaX = Math.Abs(positions[i].X - positions[j].X + 1L);
                    var deltaY = Math.Abs(positions[i].Y - positions[j].Y + 1L);

                    var area = deltaX * deltaY;

                    if (area > result1)
                    {
                        result1 = area;
                    }
                }
            }

            var uniqueXs = new SortedSet<int>();
            var uniqueYs = new SortedSet<int>();

            foreach (var position in positions)
            {
                uniqueXs.Add(position.X);
                uniqueYs.Add(position.Y);
            }

            var xCoordinateToIndex = CreateCoordinateToIndexDictionary(uniqueXs);
            var yCoordinateToIndex = CreateCoordinateToIndexDictionary(uniqueYs);

            var compressedWidth = xCoordinateToIndex.Count + 2;
            var compressedHeight = yCoordinateToIndex.Count + 2;

            var grid = PrepareGrid(positions, xCoordinateToIndex, yCoordinateToIndex, compressedWidth, compressedHeight);
            var prefixSum = new long[compressedWidth + 1, compressedHeight + 1];

            for (var i = 0; i < compressedWidth; ++i)
            {
                for (var j = 0; j < compressedHeight; ++j)
                {
                    prefixSum[i + 1, j + 1] = grid[i, j] + prefixSum[i, j + 1] + prefixSum[i + 1, j] - prefixSum[i, j];
                }
            }

            for (var i = 0; i < positions.Count - 1; ++i)
            {
                var (x1, y1) = positions[i];

                for (var j = i + 1; j < positions.Count; ++j)
                {
                    var (x2, y2) = positions[j];

                    var minX = Math.Min(x1, x2);
                    var minY = Math.Min(y1, y2);
                    var maxX = Math.Max(x1, x2);
                    var maxY = Math.Max(y1, y2);

                    var minXi = xCoordinateToIndex[minX];
                    var minYi = yCoordinateToIndex[minY];
                    var maxXi = xCoordinateToIndex[maxX];
                    var maxYi = yCoordinateToIndex[maxY];

                    var sum = AreaSumCompressed(minXi, minYi, maxXi + 1, maxYi + 1);
                    var compressedArea = (maxXi - minXi + 1L) * (maxYi - minYi + 1L);

                    if (sum == compressedArea)
                    {
                        var area = (maxX - minX + 1L) * (maxY - minY + 1L);

                        if (area > result2)
                        {
                            result2 = area;
                        }
                    }
                }

                long AreaSumCompressed(int ax, int ay, int bx, int by)
                {
                    return prefixSum[bx, by] - prefixSum[ax, by] - prefixSum[bx, ay] + prefixSum[ax, ay];
                }
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static int[,] PrepareGrid(List<(int X, int Y)> positions, Dictionary<int, int> xCoordinateToIndex, Dictionary<int, int> yCoordinateToIndex, int compressedWidth, int compressedHeight)
        {
            var grid = new int[compressedWidth, compressedHeight];

            for (var i = 0; i < compressedWidth; ++i)
            {
                for (var j = 0; j < compressedHeight; ++j)
                {
                    grid[i, j] = -1;
                }
            }

            for (var i = 0; i < positions.Count; ++i)
            {
                var firstPosition = positions[(i - 1 + positions.Count) % positions.Count];
                var secondPosition = positions[i];

                var x1 = xCoordinateToIndex[firstPosition.X];
                var y1 = yCoordinateToIndex[firstPosition.Y];
                var x2 = xCoordinateToIndex[secondPosition.X];
                var y2 = yCoordinateToIndex[secondPosition.Y];

                if (x1 == x2)
                {
                    var minY = Math.Min(y1, y2);
                    var maxY = Math.Max(y1, y2);

                    for (var j = minY; j <= maxY; ++j)
                    {
                        grid[x1, j] = 1;
                    }
                }
                else if (y1 == y2)
                {
                    var minX = Math.Min(x1, x2);
                    var maxX = Math.Max(x1, x2);

                    for (var j = minX; j <= maxX; ++j)
                    {
                        grid[j, y1] = 1;
                    }
                }
            }

            var stack = new Stack<(int X, int Y)>();

            grid[0, 0] = 0;
            stack.Push((0, 0));

            while (stack.Count > 0)
            {
                var (x, y) = stack.Pop();

                TryToMarkPosition(x + 1, y);
                TryToMarkPosition(x - 1, y);
                TryToMarkPosition(x, y + 1);
                TryToMarkPosition(x, y - 1);

                void TryToMarkPosition(int nextX, int nextY)
                {
                    if (nextX >= 0 && nextX < compressedWidth && nextY >= 0 && nextY < compressedHeight && grid[nextX, nextY] == -1)
                    {
                        grid[nextX, nextY] = 0;
                        stack.Push((nextX, nextY));
                    }
                }
            }

            for (var i = 0; i < compressedWidth; ++i)
            {
                for (var j = 0; j < compressedHeight; ++j)
                {
                    if (grid[i, j] == -1)
                    {
                        grid[i, j] = 1;
                    }
                }
            }

            return grid;
        }

        private static Dictionary<int, int> CreateCoordinateToIndexDictionary(SortedSet<int> coordinates)
        {
            var coordinateToIndex = new Dictionary<int, int>(coordinates.Count);
            var index = 1;

            foreach (var x in coordinates)
            {
                coordinateToIndex[x] = index++;
            }

            return coordinateToIndex;
        }

        private static (int, int) ParseCoordinates(string line)
        {
            var data = line.Split(',');

            return (int.Parse(data[0]), int.Parse(data[1]));
        }
    }
}
