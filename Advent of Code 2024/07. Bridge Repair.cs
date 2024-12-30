namespace AdventOfCode2024
{
    [TestClass]
    public class Day07
    {
        [TestMethod]
        [DataRow("Sample 07.txt", 3749L, 11387L, DisplayName = "Sample")]
        [DataRow("Input 07.txt", 12940396350192L, 106016735664498L, DisplayName = "Input")]
        public void Solve(string fileName, long expectedResult1, long expectedResult2)
        {
            var (result1, result2) = (0L, 0L);

            foreach (var line in File.ReadLines(fileName))
            {
                var data = line.Split([':', ' '], StringSplitOptions.RemoveEmptyEntries);

                var expectedResult = long.Parse(data[0]);
                var arguments = data[1..].Select(long.Parse).ToArray();

                var firstTryResult = ValidateEquation(arguments, expectedResult, Subtract, Divide);

                result1 += firstTryResult ? expectedResult : 0L;
                result2 += firstTryResult || ValidateEquation(arguments, expectedResult, Subtract, Divide, Deconcatenate) ? expectedResult : 0L;
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static bool ValidateEquation(long[] arguments, long expectedResult, params IEnumerable<Func<long, long, long>> operations)
        {
            var stack = new Stack<(int Index, long Result)>([(1, expectedResult)]);

            while (stack.TryPop(out var item))
            {
                if (item.Index < arguments.Length)
                {
                    var operationResults = operations.Select(o => o(item.Result, arguments[^item.Index]));

                    foreach (var result in operationResults.Where(r => r >= arguments[0]))
                    {
                        stack.Push(new(item.Index + 1, result));
                    }
                }
                else if (item.Result == arguments[0])
                {
                    return true;
                }
            }

            return false;
        }

        private static long Subtract(long difference, long subtrahend) => difference - subtrahend;

        private static long Divide(long quotient, long divisor)
        {
            if (divisor == 0 || quotient % divisor != 0)
            {
                return -1;
            }

            return quotient / divisor;
        }

        private static long Deconcatenate(long result, long suffix)
        {
            var divisor = (long)Math.Pow(10.0, (int)Math.Log10(suffix) + 1.0);

            if (result % divisor != suffix)
            {
                return -1;
            }

            return result / divisor;
        }
    }
}
