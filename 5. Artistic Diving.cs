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
            var t = int.Parse(Console.ReadLine());
            var n = int.Parse(Console.ReadLine());

            var moves = new List<(int, int)>();

            for (var i = 0; i < n; ++i)
            {
                moves.Add(ReadPair());
            }

            var score = new int[t + 1];

            foreach (var (moveTime, movePoints) in moves)
            {
                var newScore = score.ToArray();

                for (var i = 1; i * moveTime <= t; ++i)
                {
                    var currentPoints = i * movePoints - i * (i - 1) / 2;

                    if (currentPoints <= 0)
                    {
                        break;
                    }

                    for (var j = t; j >= moveTime * i; --j)
                    {
                        newScore[j] = Math.Max(newScore[j], score[j - moveTime * i] + currentPoints);
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
