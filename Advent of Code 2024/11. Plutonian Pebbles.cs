var input = File.ReadAllText("Input 11.txt");
var stones = input.Split(' ').Select(long.Parse).CountBy(x => x).ToDictionary(x => x.Key, x => (long)x.Value);

var results = CountStones(stones, 25, 75).ToList();

if (results is not [var result1, var result2])
{
    throw new Exception("Implementation error.");
}

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

static IEnumerable<long> CountStones(IDictionary<long, long> stones, params IList<int> blinks)
{
    var upperBound = blinks.Max();

    for (var i = 0; i < upperBound; ++i)
    {
        var newStones = new Dictionary<long, long>();

        foreach (var (stone, count) in stones)
        {
            if (stone == 0)
            {
                newStones[1] = newStones.GetValueOrDefault(1) + count;

                continue;
            }

            var digits = CountDigits(stone);

            if (digits % 2 != 0)
            {
                var newStone = stone * 2024L;

                newStones[newStone] = newStones.GetValueOrDefault(newStone) + count;

                continue;
            }

            var power = (long)Math.Pow(10.0, digits / 2);

            var leftStone = stone / power;
            var rightStone = stone % power;

            newStones[leftStone] = newStones.GetValueOrDefault(leftStone) + count;
            newStones[rightStone] = newStones.GetValueOrDefault(rightStone) + count;
        }

        if (blinks.Contains(i + 1))
        {
            yield return newStones.Sum(x => x.Value);
        }

        stones = newStones;
    }
}

static int CountDigits(long number) => (int)Math.Log10(number) + 1;
