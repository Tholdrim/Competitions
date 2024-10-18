using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MatchUp2024.Task03
{
    public class Program
    {
        [SuppressMessage("Style", "IDE0060:Remove unused parameter")]
        public static void Main(string[] arguments)
        {
            var (n, m) = ParsePair(Console.ReadLine());

            var directions = new char[n + 2, m + 2];
            var times = new int[n + 2, m + 2];

            for (var y = 1; y <= n; ++y)
            {
                var line = Console.ReadLine();

                for (var x = 1; x <= m; ++x)
                {
                    directions[y, x] = line[x - 1];
                    times[y, x] = int.MaxValue;
                }
            }

            var bestTime = int.MaxValue;
            var initialQueueItems = Enumerable.Range(1, m).Select(x => (x, n, 1, 'v'));
            var queue = new Queue<(int X, int Y, int Time, char Direction)>(initialQueueItems);

            while (queue.TryDequeue(out var item))
            {
                if (item.Direction != directions[item.Y, item.X] || item.Time >= times[item.Y, item.X])
                {
                    continue;
                }

                times[item.Y, item.X] = item.Time;

                if (item.Y == 1 && item.Time < bestTime)
                {
                    bestTime = item.Time;
                }

                queue.Enqueue(ValueTuple.Create(item.X, item.Y - 1, item.Time + 1, 'v'));
                queue.Enqueue(ValueTuple.Create(item.X, item.Y + 1, item.Time + 1, '^'));
                queue.Enqueue(ValueTuple.Create(item.X - 1, item.Y, item.Time + 1, '>'));
                queue.Enqueue(ValueTuple.Create(item.X + 1, item.Y, item.Time + 1, '<'));
            }

            var bestPositions = Enumerable.Range(1, m).Where(x => times[1, x] == bestTime);

            Console.WriteLine(string.Join(" ", bestPositions));
        }

        private static (int, int) ParsePair(string line)
        {
            var data = line.Split();

            return (int.Parse(data[0]), int.Parse(data[1]));
        }
    }
}
