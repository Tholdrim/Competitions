var (result1, result2) = (0L, 0L);

var input = File.ReadAllText("Input 11.txt");
var stones = input.Split(' ').Select(long.Parse).CountBy(x => x).ToDictionary(x => x.Key, x => (long)x.Value);

for (var i = 0; i < 75; ++i)
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

        if (digits % 2 == 0)
        {
            var power = (long)Math.Pow(10.0, digits / 2);

            newStones[stone / power] = newStones.GetValueOrDefault(stone / power) + count;
            newStones[stone % power] = newStones.GetValueOrDefault(stone % power) + count;
        }
        else
        {
            newStones[stone * 2024L] = newStones.GetValueOrDefault(stone * 2024L) + count;
        }
    }

    stones = newStones;

    if (i == 24)
    {
        result1 = stones.Sum(x => x.Value);
    }
}

result2 = stones.Sum(x => x.Value);

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

static int CountDigits(long number) => (int)(Math.Log10(number) + 1);
