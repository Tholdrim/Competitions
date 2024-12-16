var blocks = new List<int?>();

var input = File.ReadAllText("Input 09.txt").TrimEnd();

for (var i = 0; i < input.Length; ++i)
{
    var nextBlocks = Enumerable.Repeat<int?>(i % 2 == 0 ? i / 2 : null, input[i] - '0');

    blocks.AddRange(nextBlocks);
}

var result1 = CalculateChecksum(CompressDiskByMovingBlocks([.. blocks]));
var result2 = CalculateChecksum(CompressDiskByMovingFiles([.. blocks]));

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

static List<int?> CompressDiskByMovingBlocks(List<int?> blocks)
{
    for (var (i, j) = (0, blocks.Count - 1); i < j; ++i)
    {
        if (blocks[i] != null)
        {
            continue;
        }

        while (blocks[j] == null)
        {
            --j;
        }

        blocks[i] = blocks[j];
        blocks[j] = null;
    }

    return blocks;
}

static List<int?> CompressDiskByMovingFiles(List<int?> blocks)
{
    for (var i = blocks.Count - 1; i > 0; --i)
    {
        if (blocks[i] == null)
        {
            continue;
        }

        var firstIndex = -1;
        var additionalSpaceNeeded = GetFileSize(blocks, i);

        i -= additionalSpaceNeeded - 1;

        for (var j = 0; j < i; ++j)
        {
            if (blocks[j] != null)
            {
                firstIndex = -1;
                continue;
            }

            if (firstIndex == -1)
            {
                firstIndex = j;
            }

            if (j - firstIndex + 1 == additionalSpaceNeeded)
            {
                for (var k = 0; k < additionalSpaceNeeded; ++k)
                {
                    blocks[firstIndex + k] = blocks[i + k];
                    blocks[i + k] = null;
                }

                break;
            }
        }
    }

    return blocks;
}

static int GetFileSize(List<int?> blocks, int endIndex)
{
    var result = 1;

    while (endIndex - result > 0 && blocks[endIndex - result] == blocks[endIndex])
    {
        ++result;
    }

    return result;
}

static long CalculateChecksum(List<int?> blocks)
{
    return blocks.Index().Sum(b => b.Item.HasValue ? b.Index * b.Item.Value : 0L);
}
