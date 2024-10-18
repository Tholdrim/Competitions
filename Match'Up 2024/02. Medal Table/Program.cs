using System;
using System.Diagnostics.CodeAnalysis;

namespace MatchUp2024.Task02
{
    public static class Program
    {
        [SuppressMessage("Style", "IDE0060:Remove unused parameter")]
        public static void Main(string[] arguments)
        {
            var n = int.Parse(Console.ReadLine());
            var bestEntry = ParseEntry(Console.ReadLine());

            for (var i = 1; i < n; ++i)
            {
                var entry = ParseEntry(Console.ReadLine());

                if (entry.Gold > bestEntry.Gold ||
                    entry.Gold == bestEntry.Gold && entry.Silver > bestEntry.Silver ||
                    entry.Gold == bestEntry.Gold && entry.Silver == bestEntry.Silver && entry.Bronze > bestEntry.Bronze)
                {
                    bestEntry = entry;
                }
            }

            Console.WriteLine(bestEntry.Country);
        }

        private static (string Country, int Gold, int Silver, int Bronze) ParseEntry(string line)
        {
            var data = line.Split();

            return (data[0], int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]));
        }
    }
}
