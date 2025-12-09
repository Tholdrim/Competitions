using System;
using System.Collections.Generic;

namespace LeMatchUp.Year2024.Task04
{
    public class Program
    {
        public static void Main(string[] _)
        {
            var maxReach = int.Parse(Console.ReadLine());

            var startingHold = ParsePair(Console.ReadLine());
            var finishingHold = ParsePair(Console.ReadLine());

            var holdCount = int.Parse(Console.ReadLine());
            var holds = new List<(int X, int Y)>(holdCount + 2) { startingHold };

            for (var i = 0; i < holdCount; ++i)
            {
                holds.Add(ParsePair(Console.ReadLine()));
            }

            holds.Add(finishingHold);

            var shortestRoute = FindShortestRoute(holds, maxReach);

            if (shortestRoute == null)
            {
                Console.WriteLine("-1");

                return;
            }

            foreach (var index in shortestRoute)
            {
                Console.WriteLine($"{holds[index].X} {holds[index].Y}");
            }
        }

        private static Stack<int> FindShortestRoute(List<(int X, int Y)> holds, int maxReach)
        {
            var queue = new Queue<int>(new[] { 0 });
            var predecessors = new int?[holds.Count];
            var maxReachSquared = maxReach * maxReach;

            while (queue.Count > 0)
            {
                var currentIndex = queue.Dequeue();

                if (currentIndex == holds.Count - 1)
                {
                    return ReconstructPath(predecessors);
                }

                for (var i = 1; i < holds.Count; ++i)
                {
                    if (predecessors[i] == null && IsWithinReach(holds[currentIndex], holds[i], maxReachSquared))
                    {
                        predecessors[i] = currentIndex;
                        queue.Enqueue(i);
                    }
                }
            }

            return null;
        }

        private static Stack<int> ReconstructPath(int?[] predecessors)
        {
            var path = new Stack<int>();
            int? currentIndex = predecessors.Length - 1;

            while (currentIndex != null)
            {
                path.Push(currentIndex.Value);
                currentIndex = predecessors[currentIndex.Value];
            }

            return path;
        }

        private static bool IsWithinReach((int X, int Y) sourceHold, (int X, int Y) targetHold, int maxReachSquared)
        {
            var deltaX = sourceHold.X - targetHold.X;
            var deltaY = sourceHold.Y - targetHold.Y;

            return deltaX * deltaX + deltaY * deltaY <= maxReachSquared;
        }

        private static (int, int) ParsePair(string line)
        {
            var data = line.Split();

            return (int.Parse(data[0]), int.Parse(data[1]));
        }
    }
}
