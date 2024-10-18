using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MatchUp2024.Task04
{
    public class Program
    {
        [SuppressMessage("Style", "IDE0060:Remove unused parameter")]
        public static void Main(string[] arguments)
        {
            var reach = int.Parse(Console.ReadLine());

            var startingHold = ParsePair(Console.ReadLine());
            var finishingHold = ParsePair(Console.ReadLine());

            var n = int.Parse(Console.ReadLine()) + 2;
            var holds = new List<(int X, int Y)>(n) { startingHold };

            for (var i = 1; i < n - 1; ++i)
            {
                holds.Add(ParsePair(Console.ReadLine()));
            }

            holds.Add(finishingHold);

            var visited = new bool[n];
            var queue = new Queue<List<int>>(new[] { new List<int> { 0 } });

            while (queue.TryDequeue(out var path))
            {
                var index = path[^1];

                if (index == n - 1)
                {
                    for (var i = 0; i < path.Count; ++i)
                    {
                        Console.WriteLine($"{holds[path[i]].X} {holds[path[i]].Y}");
                    }

                    return;
                }

                for (var i = 1; i < n; ++i)
                {
                    if (!visited[i] && CheckIfWithinReach(holds[index], holds[i], reach))
                    {
                        visited[i] = true;

                        queue.Enqueue(path.Append(i).ToList());
                    }
                }
            }

            Console.WriteLine("-1");
        }

        private static bool CheckIfWithinReach((int X, int Y) first, (int X, int Y) second, int reach)
        {
            return (first.X - second.X) * (first.X - second.X) + (first.Y - second.Y) * (first.Y - second.Y) <= reach * reach;
        }

        private static (int, int) ParsePair(string line)
        {
            var data = line.Split();

            return (int.Parse(data[0]), int.Parse(data[1]));
        }
    }
}
