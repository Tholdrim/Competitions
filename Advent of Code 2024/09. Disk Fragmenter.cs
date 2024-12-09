var input = File.ReadAllText("Input 09.txt");

var fileId = 0;
var isFreeSpace = false;
var diskBlocks = new List<int?>();

for (var i = 0; i < input.Length; ++i)
{
    for (var j = 0; j < input[i] - '0'; ++j)
    {
        diskBlocks.Add(isFreeSpace ? null : fileId);
    }

    fileId += isFreeSpace ? 0 : 1;
    isFreeSpace = !isFreeSpace;
}

var result1 = ComputePart1Checksum([.. diskBlocks]);
var result2 = ComputePart2Checksum([.. diskBlocks]);

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

static long ComputePart1Checksum(IList<int?> blocks)
{
    var result = 0L;

    for (var i = 0; i < blocks.Count; ++i)
    {
        if (blocks[i] != null)
        {
            continue;
        }

        for (var j = blocks.Count - 1; j > i; --j)
        {
            if (blocks[j] == null)
            {
                continue;
            }

            (blocks[i], blocks[j]) = (blocks[j], null);

            break;
        }
    }

    for (var i = 0; i < blocks.Count; ++i)
    {
        if (blocks[i] is int currentFileId)
        {
            result += i * currentFileId;
        }
    }

    return result;
}

static long ComputePart2Checksum(IList<int?> blocks)
{
    var result = 0L;

    for (var i = blocks.Count - 1; i > 0; --i)
    {
        if (blocks[i] == null)
        {
            continue;
        }

        var firstIndex = -1;
        var spaceNeeded = blocks.Where(b => b == blocks[i]).Count();

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
            
            if (j - firstIndex + 1 == spaceNeeded)
            {
                for (var k = 0; k < spaceNeeded; ++k)
                {
                    (blocks[i - k], blocks[firstIndex + k]) = (blocks[firstIndex + k], blocks[i - k]);
                }

                break;
            }
        }
    }

    for (var i = 0; i < blocks.Count; ++i)
    {
        if (blocks[i] is int currentFileId)
        {
            result += i * currentFileId;
        }
    }

    return result;
}
