namespace AdventOfCode2024
{
    using FileBlock = (int FileId, int Index, int Length);

    [TestClass]
    public class Day09
    {
        private const int FreeSpace = -1;

        [TestMethod]
        [DataRow("Sample 09.txt", 1928L, 2858L, DisplayName = "Sample")]
        [DataRow("Input 09.txt", 6356833654075L, 6389911791746L, DisplayName = "Input")]
        public void Solve(string fileName, long expectedResult1, long expectedResult2)
        {
            var input = File.ReadAllText(fileName).AsSpan().TrimEnd();

            ProcessInput(input, out var blocks, out var fileBlocks, out var freeSpaceBlocks);

            var result1 = CalculateChecksumAfterBlockCompaction(blocks);
            var result2 = CalculateChecksumAfterFileCompaction(fileBlocks, freeSpaceBlocks);

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static long CalculateChecksumAfterBlockCompaction(ReadOnlySpan<int> blocks)
        {
            var result = 0L;

            for (var (i, j) = (0, blocks.Length - 1); i <= j; ++i)
            {
                if (blocks[i] != FreeSpace)
                {
                    result += i * blocks[i];

                    continue;
                }

                while (blocks[j] == FreeSpace)
                {
                    --j;
                }

                result += i * blocks[j--];
            }

            return result;
        }

        private static long CalculateChecksumAfterFileCompaction(Stack<FileBlock> fileBlocks, SortedSet<int>[] freeSpaceBlocks)
        {
            var result = 0L;

            while (fileBlocks.TryPop(out var block))
            {
                var (fileId, sourceIndex, length) = block;
                var (destinationIndex, freeSpaceBlockLength) = (sourceIndex, -1);

                for (var i = length - 1; i < freeSpaceBlocks.Length; ++i)
                {
                    if (freeSpaceBlocks[i].Count > 0 && freeSpaceBlocks[i].Min < destinationIndex)
                    {
                        freeSpaceBlockLength = i;
                        destinationIndex = freeSpaceBlocks[i].Min;
                    }
                }

                if (freeSpaceBlockLength > -1)
                {
                    if (freeSpaceBlockLength - length >= 0)
                    {
                        freeSpaceBlocks[freeSpaceBlockLength - length].Add(destinationIndex + length);
                    }

                    freeSpaceBlocks[freeSpaceBlockLength].Remove(destinationIndex);
                }

                result += (long)fileId * (length * destinationIndex + length * (length - 1) / 2);
            }

            return result;
        }

        private static void ProcessInput(
            ReadOnlySpan<char> input,
            out ReadOnlySpan<int> blocks,
            out Stack<FileBlock> fileBlocks,
            out SortedSet<int>[] freeSpaceBlocks)
        {
            var index = 0;
            var blocksArray = new int[input.Length * 9];

            fileBlocks = new Stack<(int FileId, int Index, int Length)>();
            freeSpaceBlocks = Enumerable.Range(0, 9).Select(_ => new SortedSet<int>()).ToArray();

            for (var i = 0; i < input.Length; ++i)
            {
                var length = input[i] - '0';

                if (length == 0)
                {
                    continue;
                }

                switch (i % 2)
                {
                    case 0:
                        fileBlocks.Push(new(i / 2, index, length));
                        Array.Fill(blocksArray, i / 2, index, length);
                        break;

                    case 1:
                        freeSpaceBlocks[length - 1].Add(index);
                        Array.Fill(blocksArray, FreeSpace, index, length);
                        break;
                }

                index += length;
            }

            blocks = blocksArray.AsSpan()[..index];
        }
    }
}
