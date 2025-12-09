using System;
using System.Linq;

namespace LeMatchUp.Year2024.Task01
{
    public static class Program
    {
        public static void Main(string[] _)
        {
            var badgeCount = int.Parse(Console.ReadLine());

            for (var i = 0; i < badgeCount; ++i)
            {
                var badgeNumber = Console.ReadLine();

                if (badgeNumber.StartsWith("42") && badgeNumber.Sum(d => d - '0') == 75)
                {
                    Console.WriteLine(badgeNumber);

                    return;
                }
            }
        }
    }
}
