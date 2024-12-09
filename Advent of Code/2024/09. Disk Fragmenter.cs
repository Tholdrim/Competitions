namespace AdventOfCode.Year2024
{
    [TestClass]
    public class Day09
    {
        [TestMethod]
        [DataRow("Data/Sample 09.txt", 1928L, 2858L, DisplayName = "Sample")]
        [DataRow("Data/Input 09.secret", 6356833654075L, 6389911791746L, DisplayName = "Input")]
        public void Solve(string fileName, long expectedResult1, long expectedResult2)
        {
            var input = File.ReadAllText(fileName).AsSpan().TrimEnd();

            ProcessInput(input, out var blocks, out var files, out var freeSpaceSpans);

            var result1 = CalculateChecksumAfterBlockCompaction(blocks);
            var result2 = CalculateChecksumAfterFileCompaction(files, freeSpaceSpans);

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static long CalculateChecksumAfterBlockCompaction(ReadOnlySpan<short> blocks)
        {
            var result = 0L;

            for (var (i, j) = (0, blocks.Length - 1); i <= j; ++i)
            {
                if (blocks[i] != -1)
                {
                    result += i * blocks[i];

                    continue;
                }

                while (blocks[j] == -1)
                {
                    --j;
                }

                result += i * blocks[j--];
            }

            return result;
        }

        private static long CalculateChecksumAfterFileCompaction(Stack<FileInfo> files, SortedSet<int>[] freeSpaceSpans)
        {
            var result = 0L;

            while (files.TryPop(out var file))
            {
                var (fileId, sourcePosition, length) = file;
                var (destinationPosition, freeSpaceBlockLength) = (sourcePosition, -1);

                for (var i = length - 1; i < freeSpaceSpans.Length; ++i)
                {
                    if (freeSpaceSpans[i].Count > 0 && freeSpaceSpans[i].Min < destinationPosition)
                    {
                        freeSpaceBlockLength = i;
                        destinationPosition = freeSpaceSpans[i].Min;
                    }
                }

                if (freeSpaceBlockLength != -1)
                {
                    if (freeSpaceBlockLength - length >= 0)
                    {
                        freeSpaceSpans[freeSpaceBlockLength - length].Add(destinationPosition + length);
                    }

                    freeSpaceSpans[freeSpaceBlockLength].Remove(destinationPosition);
                }

                result += (long)fileId * (length * destinationPosition + length * (length - 1) / 2);
            }

            return result;
        }

        private static void ProcessInput(
            ReadOnlySpan<char> input,
            out ReadOnlySpan<short> blocks,
            out Stack<FileInfo> files,
            out SortedSet<int>[] freeSpaceSpans)
        {
            var blockIndex = 0;
            var blocksArray = new short[input.Length * 9];
            var freeSpaceLists = Enumerable.Range(0, 9).Select(_ => new List<int>()).ToArray();

            files = new Stack<FileInfo>();

            for (var i = 0; i < input.Length; ++i)
            {
                var length = input[i] - '0';

                if (length == 0)
                {
                    continue;
                }

                if (i % 2 == 0)
                {
                    files.Push(new FileInfo(i / 2, blockIndex, length));
                    Array.Fill(blocksArray, (short)(i / 2), blockIndex, length);
                }
                else
                {
                    freeSpaceLists[length - 1].Add(blockIndex);
                    Array.Fill(blocksArray, (short)-1, blockIndex, length);
                }

                blockIndex += length;
            }

            blocks = blocksArray.AsSpan()[..blockIndex];
            freeSpaceSpans = freeSpaceLists.Select(l => new SortedSet<int>(l)).ToArray();
        }

        private readonly record struct FileInfo(int Identifier, int Position, int Length);
    }
}
