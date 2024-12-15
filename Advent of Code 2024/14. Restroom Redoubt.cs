using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

var lines = File.ReadAllLines("Input 14.txt");

const int width = 101;
const int height = 103;

var (result1, result2) = (SolvePart1(lines), SolvePart2(lines));

static int SolvePart1(string[] lines)
{
    var quadrants = new List<int>([0, 0, 0, 0]);

    foreach (var line in lines)
    {
        var match = Regex.Match(line);
        var (px, py) = (int.Parse(match.Groups["px"].Value), int.Parse(match.Groups["py"].Value));
        var (vx, vy) = (int.Parse(match.Groups["vx"].Value), int.Parse(match.Groups["vy"].Value));

        var (x, y) = (((px + vx * 100) % width + width) % width, ((py + vy * 100) % height + height) % height);

        if (x == width / 2 || y == height / 2)
        {
            continue;
        }

        quadrants[(x > width / 2 ? 1 : 0) + (y > height / 2 ? 2 : 0)]++;
    }

    return quadrants.Aggregate((r, q) => r * q);
}

static int SolvePart2(string[] lines)
{
    var robots = new List<(int PX, int PY, int DX, int DY)>();

    foreach (var line in lines)
    {
        var match = Regex.Match(line);
        var (px, py) = (int.Parse(match.Groups["px"].Value), int.Parse(match.Groups["py"].Value));
        var (vx, vy) = (int.Parse(match.Groups["vx"].Value), int.Parse(match.Groups["vy"].Value));

        robots.Add((px, py, vx, vy));
    }

    // This magic number requires observing the first ~300 seconds.
    for (var i = 0; i < 33; ++i)
    {
        for (var j = 0; j < robots.Count; ++j)
        {
            var px = (robots[j].PX + width + robots[j].DX) % width;
            var py = (robots[j].PY + height + robots[j].DY) % height;

            robots[j] = (px, py, robots[j].DX, robots[j].DY);
        }
    }

    for (var i = 0; i < 100; ++i)
    {
        var bitmap = new Bitmap(width, height);
        var outputGraphics = Graphics.FromImage(bitmap);

        outputGraphics.Clear(Color.Black);

        for (var j = 0; j < robots.Count; ++j)
        {
            var px = ((robots[j].PX + robots[j].DX * 101) % width + width) % width;
            var py = ((robots[j].PY + robots[j].DY * 101) % height + height) % height;

            bitmap.SetPixel(px, py, Color.Yellow);

            robots[j] = (px, py, robots[j].DX, robots[j].DY);
        }

        using var stream = File.Create($"Output/Output-{i + 1}.png");

        bitmap.Save(stream, ImageFormat.Png);
    }

    return 0;
}

Console.WriteLine($"Part 1: {result1}");
Console.WriteLine($"Part 2: {result2}");

internal partial class Program
{
    [GeneratedRegex(@"p=(?<px>-?\d+),(?<py>-?\d+) v=(?<vx>-?\d+),(?<vy>\-?\d+)")]
    private static partial Regex Regex { get; }
}
