using System;

namespace LeMatchUp.Year2024.Task02
{
    public static class Program
    {
        public static void Main(string[] _)
        {
            var countryCount = int.Parse(Console.ReadLine());
            var bestEntry = ParseEntry(Console.ReadLine());

            for (var i = 1; i < countryCount; ++i)
            {
                var currentEntry = ParseEntry(Console.ReadLine());

                if (currentEntry.CompareTo(bestEntry) > 0)
                {
                    bestEntry = currentEntry;
                }
            }

            Console.WriteLine(bestEntry.Country);
        }

        private static (int Gold, int Silver, int Bronze, string Country) ParseEntry(string line)
        {
            var data = line.Split();

            return (int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]), data[0]);
        }
    }
}
