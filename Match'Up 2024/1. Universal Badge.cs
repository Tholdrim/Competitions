using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MatchUp2024
{
    internal class Program
    {
        [SuppressMessage("Style", "IDE0060:Remove unused parameter")]
        public static void Main(string[] arguments)
        {
            var n = int.Parse(Console.ReadLine());

            for (var i = 0; i < n; ++i)
            {
                var line = Console.ReadLine();

                if (!line.StartsWith("42"))
                {
                    continue;
                }

                var sum = line.Select(d => d - '0').Sum();

                if (sum == 75)
                {
                    Console.WriteLine(line);
                    
                    return;
                }
            }
        }
    }
}
