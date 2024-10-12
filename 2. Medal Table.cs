using System;
using System.Diagnostics.CodeAnalysis;

namespace Competition
{
    internal class Program
    {
        [SuppressMessage("Style", "IDE0060:Remove unused parameter")]
        public static void Main(string[] arguments)
        {
            var bestEntry = new Entry();

            var n = int.Parse(Console.ReadLine());

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

    internal struct Entry
    {
        public string Country { get; set; }

        public int Gold { get; set; }

        public int Silver { get; set; }

        public int Bronze { get; set; }

        public static Entry Parse(string line)
        {
            var data = line.Split();

            return new Entry()
            {
                Country = data[0],
                Gold = int.Parse(data[1]),
                Silver = int.Parse(data[2]),
                Bronze = int.Parse(data[3])
            };
        }
    }
}
