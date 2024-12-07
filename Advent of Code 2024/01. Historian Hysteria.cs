var leftList = new List<int>();
var rightList = new List<int>();

await foreach (var line in File.ReadLinesAsync("Input 01.txt"))
{
    var data = line.Split("   ");

    leftList.Add(int.Parse(data[0]));
    rightList.Add(int.Parse(data[1]));
}

leftList.Sort();
rightList.Sort();

var rightCount = rightList.CountBy(n => n).ToDictionary(n => n.Key, n => n.Value);

var result1 = leftList.Zip(rightList, (x, y) => Math.Abs(x - y)).Sum();
var result2 = leftList.Sum(n => n * rightCount.GetValueOrDefault(n));

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");
