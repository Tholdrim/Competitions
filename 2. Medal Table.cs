using System;
using System.Diagnostics.CodeAnalysis;

namespace Competition
{
    internal class Program
    {
        [SuppressMessage("Style", "IDE0060:Remove unused parameter")]
        public static void Main(string[] arguments)
        {
            var n = int.Parse(Console.ReadLine());

            var bestEntry = new Entry();

            for (var i = 0; i < n; ++i)
            {
                var entry = Entry.Parse(Console.ReadLine());

                if (entry.Gold > bestEntry.Gold ||
                    entry.Gold == bestEntry.Gold && entry.Silver > bestEntry.Silver ||
                    entry.Gold == bestEntry.Gold && entry.Silver == bestEntry.Silver && entry.Bronze > bestEntry.Bronze)
                {
                    bestEntry = entry;
                }
            }

            Console.WriteLine(bestEntry.Country);
        }
    }

    internal readonly struct Entry
    {
        public string Country { get; }

        public int Gold { get; }

        public int Silver { get; }

        public int Bronze { get; }

        private Entry(string country, int gold, int silver, int bronze)
        {
            (Country, Gold, Silver, Bronze) = (country, gold, silver, bronze);
        }

        public static Entry Parse(string line)
        {
            var data = line.Split();

            return new Entry(data[0], int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]));
        }
    }
}
