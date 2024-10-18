using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Competition
{
    internal class Program
    {
        [SuppressMessage("Style", "IDE0060:Remove unused parameter")]
        public static void Main(string[] arguments)
        {
            var reach = int.Parse(Console.ReadLine());

            var startingHold = ReadPair();
            var finishingHold = ReadPair();

            var n = int.Parse(Console.ReadLine()) + 2;
            var holds = new List<(int X, int Y)>(n) { startingHold };

            for (var i = 1; i < n - 1; ++i)
            {
                holds.Add(ReadPair());
            }

            holds.Add(finishingHold);

            var visited = new bool[n];
            var queue = new Queue<List<int>>(new[] { new List<int> { 0 } });

            while (queue.TryDequeue(out var path))
            {
                var currentIndex = path.Last();

                if (currentIndex == n - 1)
                {
                    foreach (var index in path)
                    {
                        Console.WriteLine($"{holds[index].X} {holds[index].Y}");
                    }

                    return;
                }

                for (var i = 1; i < n; ++i)
                {
                    if (!visited[i] && CheckIfWithinReach(holds[currentIndex], holds[i], reach))
                    {
                        visited[i] = true;

                        queue.Enqueue(new List<int>(path) { i });
                    }
                }
            }

            Console.WriteLine("-1");
        }

        private static bool CheckIfWithinReach((int X, int Y) first, (int X, int Y) second, int reach)
        {
            return (first.X - second.X) * (first.X - second.X) + (first.Y - second.Y) * (first.Y - second.Y) <= reach * reach;
        }

        private static (int, int) ReadPair()
        {
            var data = Console.ReadLine().Split();

            return (int.Parse(data[0]), int.Parse(data[1]));
        }
    }
}
