using System.Runtime.InteropServices;

namespace AdventOfCode.Year2024
{
    [TestClass]
    public class Day07
    {
        private static readonly Memory<Func<long, long, long>> TwoOperations = new[] { Subtract, Divide };
        private static readonly Memory<Func<long, long, long>> AllOperations = new[] { Subtract, Divide, Deconcatenate };

        [TestMethod]
        [DataRow("Data/Sample 07.txt", 3749L, 11387L, DisplayName = "Sample")]
        [DataRow("Data/Input 07.secret", 12940396350192L, 106016735664498L, DisplayName = "Input")]
        public void Solve(string fileName, long expectedResult1, long expectedResult2)
        {
            var list = new List<long>();
            var (result1, result2) = (0L, 0L);

            foreach (ReadOnlySpan<char> line in File.ReadLines(fileName))
            {
                foreach (var range in line.SplitAny(": "))
                {
                    if (long.TryParse(line[range], out var value))
                    {
                        list.Add(value);
                    }
                }

                var expectedResult = list[0];
                var arguments = CollectionsMarshal.AsSpan(list)[1..];
                var firstTryResult = ValidateEquation(arguments, expectedResult, TwoOperations);

                result1 += firstTryResult ? expectedResult : 0L;
                result2 += firstTryResult || ValidateEquation(arguments, expectedResult, AllOperations) ? expectedResult : 0L;

                list.Clear();
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static bool ValidateEquation(ReadOnlySpan<long> arguments, long expectedResult, Memory<Func<long, long, long>> operations)
        {
            var stack = new Stack<(int Index, long Result)>([new(1, expectedResult)]);

            while (stack.TryPop(out var item))
            {
                if (item.Index < arguments.Length)
                {
                    foreach (var operation in operations.Span)
                    {
                        var result = operation(item.Result, arguments[^item.Index]);

                        if (result >= arguments[0])
                        {
                            stack.Push(new(item.Index + 1, result));
                        }
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

        private static long Divide(long quotient, long divisor) => divisor > 0 && quotient % divisor == 0 ? quotient / divisor : -1L;

        private static long Deconcatenate(long result, long suffix)
        {
            var divisor = (long)Math.Pow(10.0, (int)Math.Log10(suffix) + 1.0);

            return result % divisor == suffix ? result / divisor : -1L;
        }
    }
}
