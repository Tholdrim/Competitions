using System;
using System.Collections.Generic;
using System.Linq;

namespace LeMatchUp.Year2024.Task03
{
    public class Program
    {
        public static void Main(string[] _)
        {
            var (rows, columns) = ParsePair(Console.ReadLine());

            var arrivalTimes = new int[rows + 2, columns + 2];
            var directions = new char[rows + 2, columns + 2];

            for (var y = 1; y <= rows; ++y)
            {
                var rowDirections = Console.ReadLine();

                for (var x = 1; x <= columns; ++x)
                {
                    arrivalTimes[y, x] = int.MaxValue;
                    directions[y, x] = rowDirections[x - 1];
                }
            }

            var bestPositions = FindBestPositions(rows, columns, directions, arrivalTimes);

            Console.WriteLine(string.Join(" ", bestPositions));
        }

        private static IEnumerable<int> FindBestPositions(int rows, int columns, char[,] directions, int[,] arrivalTimes)
        {
            var bestTime = int.MaxValue;
            var initialQueueItems = Enumerable.Range(1, columns).Select(x => (x, rows, 'v', 1));
            var queue = new Queue<(int X, int Y, char Direction, int Time)>(initialQueueItems);

            while (queue.TryDequeue(out var item))
            {
                if (item.Direction != directions[item.Y, item.X] || item.Time >= arrivalTimes[item.Y, item.X])
                {
                    continue;
                }

                arrivalTimes[item.Y, item.X] = item.Time;

                if (item.Y == 1 && item.Time < bestTime)
                {
                    bestTime = item.Time;
                }

                queue.Enqueue(ValueTuple.Create(item.X, item.Y - 1, 'v', item.Time + 1));
                queue.Enqueue(ValueTuple.Create(item.X, item.Y + 1, '^', item.Time + 1));
                queue.Enqueue(ValueTuple.Create(item.X - 1, item.Y, '>', item.Time + 1));
                queue.Enqueue(ValueTuple.Create(item.X + 1, item.Y, '<', item.Time + 1));
            }

            return Enumerable.Range(1, columns).Where(x => arrivalTimes[1, x] == bestTime);
        }

        private static (int, int) ParsePair(string line)
        {
            var data = line.Split();

            return (int.Parse(data[0]), int.Parse(data[1]));
        }
    }
}
