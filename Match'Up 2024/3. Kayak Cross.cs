using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Competition
{
    internal class Program
    {
        private static int BestTime { get; set; } = int.MaxValue;

        [SuppressMessage("Style", "IDE0060:Remove unused parameter")]
        public static void Main(string[] arguments)
        {
            var (n, m) = ReadPair();

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

            var initialQueueElements = Enumerable.Range(1, m).Select(x => new QueueElement(x, n, 1, 'v'));
            var queue = new Queue<QueueElement>(initialQueueElements);

            while (queue.TryDequeue(out var element))
            {
                if (element.Direction != directions[element.Y, element.X] || element.Time >= times[element.Y, element.X])
                {
                    continue;
                }

                times[element.Y, element.X] = element.Time;

                if (element.Y == 1)
                {
                    BestTime = Math.Min(BestTime, element.Time);
                }

                queue.Enqueue(new QueueElement(element.X, element.Y - 1, element.Time + 1, 'v'));
                queue.Enqueue(new QueueElement(element.X, element.Y + 1, element.Time + 1, '^'));
                queue.Enqueue(new QueueElement(element.X - 1, element.Y, element.Time + 1, '>'));
                queue.Enqueue(new QueueElement(element.X + 1, element.Y, element.Time + 1, '<'));
            }

            var bestPositions = Enumerable.Range(1, m).Where(x => times[1, x] == BestTime);

            Console.WriteLine(string.Join(" ", bestPositions));
        }

        private static (int, int) ReadPair()
        {
            var data = Console.ReadLine().Split();

            return (int.Parse(data[0]), int.Parse(data[1]));
        }
    }

    internal readonly struct QueueElement
    {
        public QueueElement(int x, int y, int time, char direction)
        {
            (X, Y, Time, Direction) = (x, y, time, direction);
        }

        public int X { get; }

        public int Y { get; }

        public int Time { get; }

        public char Direction { get; }
    }
}
