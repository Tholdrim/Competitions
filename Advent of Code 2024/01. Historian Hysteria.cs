var leftList = new List<int>();
var rightList = new List<int>();

foreach (var line in File.ReadLines("Input 01.txt"))
{
    var data = line.Split("   ");

    leftList.Add(int.Parse(data[0]));
    rightList.Add(int.Parse(data[1]));
}

leftList.Sort();
rightList.Sort();

var rightListElementCounts = rightList.CountBy(e => e).ToDictionary(e => e.Key, e => e.Value);

var result1 = leftList.Zip(rightList, (l, r) => Math.Abs(l - r)).Sum();
var result2 = leftList.Sum(e => e * rightListElementCounts.GetValueOrDefault(e));

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");
