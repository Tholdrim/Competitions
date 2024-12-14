var input = File.ReadAllText("Input 11.txt");
var stones = input.Split(' ').Select(long.Parse).CountBy(x => x).ToDictionary(x => x.Key, x => (long)x.Value);

if (CountStones(stones, blinks: [25, 75]) is not [var result1, var result2])
{
    throw new Exception("Implementation error.");
}

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

static IList<long> CountStones(IDictionary<long, long> stones, IList<int> blinks, int blink = 1)
{
    var newStones = new Dictionary<long, long>();

    foreach (var (stone, count) in stones)
    {
        var digits = CountDigits(stone);

        switch (stone, digits % 2 == 0)
        {
            case (0L, _):
                IncrementValue(newStones, 1L, count);
                break;

            case (_, false):
                IncrementValue(newStones, stone * 2024L, count);
                break;

            case (_, true) when (long)Math.Pow(10.0, digits / 2) is var power:
                IncrementValue(newStones, stone / power, count);
                IncrementValue(newStones, stone % power, count);
                break;
        }
    }

    if (!blinks.Contains(blink))
    {
        return CountStones(newStones, blinks, blink + 1);
    }

    blinks.Remove(blink);

    return [newStones.Sum(x => x.Value), .. blinks.Count > 0 ? CountStones(newStones, blinks, blink + 1) : []];
}

static int CountDigits(long number) => (number > 0 ? (int)Math.Log10(number) : 0) + 1;

static void IncrementValue(Dictionary<long, long> dictionary, long key, long increment)
{
    dictionary[key] = dictionary.GetValueOrDefault(key, 0L) + increment;
}
