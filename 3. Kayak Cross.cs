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
            var data = Console.ReadLine().Split();

            var n = int.Parse(data[0]);
            var m = int.Parse(data[1]);

            var time = new int[n + 2, m + 2];
            var directions = new char[n + 2, m + 2];

            for (var y = 1; y <= n; ++y)
            {
                var line = Console.ReadLine();

                for (var x = 1; x <= m; ++x)
                {
                    time[y, x] = int.MaxValue;
                    directions[y, x] = line[x - 1];
                }
            }

            var bestTime = int.MaxValue;
            var queue = new Queue<QueueElement>(Enumerable.Range(1, m).Select(x => new QueueElement(x, n, 1, 'v')));

            while (queue.TryDequeue(out var element))
            {
                if (element.Direction != directions[element.Y, element.X] || element.Time >= time[element.Y, element.X])
                {
                    continue;
                }

                time[element.Y, element.X] = element.Time;

                if (element.Y == 1)
                {
                    bestTime = Math.Min(bestTime, element.Time);
                }

                queue.Enqueue(new QueueElement(element.X, element.Y - 1, element.Time + 1, 'v'));
                queue.Enqueue(new QueueElement(element.X, element.Y + 1, element.Time + 1, '^'));
                queue.Enqueue(new QueueElement(element.X - 1, element.Y, element.Time + 1, '>'));
                queue.Enqueue(new QueueElement(element.X + 1, element.Y, element.Time + 1, '<'));
            }

            var bestPositions = Enumerable.Range(1, m).Where(x => time[1, x] == bestTime);

            Console.WriteLine(string.Join(" ", bestPositions));
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
