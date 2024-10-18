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
            var t = int.Parse(Console.ReadLine());
            var n = int.Parse(Console.ReadLine());

            var score = new int[t + 1];

            for (var i = 0; i < n; ++i)
            {
                var newScore = score.ToArray();
                var (moveTime, movePoints) = ReadPair();

                for (var j = 1; j * moveTime <= t; ++j)
                {
                    var currentPoints = j * movePoints - j * (j - 1) / 2;

                    if (currentPoints <= 0)
                    {
                        break;
                    }

                    for (var k = t; k >= moveTime * j; --k)
                    {
                        newScore[k] = Math.Max(newScore[k], score[k - moveTime * j] + currentPoints);
                    }
                }

                score = newScore;
            }

            Console.WriteLine(score[t]);
        }

        private static (int, int) ReadPair()
        {
            var data = Console.ReadLine().Split();

            return (int.Parse(data[0]), int.Parse(data[1]));
        }
    }
}
