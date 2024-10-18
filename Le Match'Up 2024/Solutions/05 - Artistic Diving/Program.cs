using System;
using System.Linq;

namespace LeMatchUp.Year2024.Task05
{
    public class Program
    {
        public static void Main(string[] _)
        {
            var totalTime = int.Parse(Console.ReadLine());
            var moveCount = int.Parse(Console.ReadLine());

            var maxScoreAtTime = new int[totalTime + 1];

            for (var i = 0; i < moveCount; ++i)
            {
                var (moveTime, movePoints) = ParsePair(Console.ReadLine());

                maxScoreAtTime = UpdateScoresWithMove(totalTime, maxScoreAtTime, moveTime, movePoints);
            }

            Console.WriteLine(maxScoreAtTime[totalTime]);
        }

        private static int[] UpdateScoresWithMove(int totalTime, int[] maxScoreAtTime, int moveTime, int movePoints)
        {
            var updatedScores = maxScoreAtTime.ToArray();

            for (var i = 1; i * moveTime <= totalTime; ++i)
            {
                var repetitionPoints = i * movePoints - i * (i - 1) / 2;

                if (repetitionPoints <= 0)
                {
                    break;
                }

                for (var j = totalTime; j >= i * moveTime; --j)
                {
                    updatedScores[j] = Math.Max(updatedScores[j], maxScoreAtTime[j - i * moveTime] + repetitionPoints);
                }
            }

            return updatedScores;
        }

        private static (int, int) ParsePair(string line)
        {
            var data = line.Split();

            return (int.Parse(data[0]), int.Parse(data[1]));
        }
    }
}
