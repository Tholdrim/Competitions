namespace AdventOfCode2025
{
    [TestClass]
    public class Day06
    {
        [TestMethod]
        [DataRow("Sample 06.txt", 4277556L, 3263827L, DisplayName = "Sample")]
        [DataRow("Input 06.txt", 4449991244405L, 9348430857627L, DisplayName = "Input")]
        public void Solve(string fileName, long expectedResult1, long expectedResult2)
        {
            var lines = File.ReadAllLines(fileName);

            var (result1, result2) = (CalculatePart1(lines), CalculatePart2(lines));

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static long CalculatePart1(string[] lines)
        {
            var operators = lines[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var numbers = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

            foreach (var line in lines[1..^1])
            {
                var arguments = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                for (var i = 0; i < arguments.Length; ++i)
                {
                    numbers[i] = operators[i] switch
                    {
                        "+" => numbers[i] + long.Parse(arguments[i]),
                        "*" => numbers[i] * long.Parse(arguments[i]),
                        _   => throw new NotImplementedException()
                    };
                }
            }

            return numbers.Sum();
        }

        private static long CalculatePart2(string[] lines)
        {
            var result = 0L;
            var operators = lines[^1];
            var numbers = new List<long>();

            for (var i = operators.Length - 1; i >= 0; --i)
            {
                var column = Enumerable.Range(0, lines.Length - 1).Select(j => lines[j][i]).ToArray();

                if (!long.TryParse(new string(column), out var number))
                {
                    continue;
                }

                numbers.Add(number);

                if (operators[i] == '+')
                {
                    result += numbers.Sum();
                    numbers.Clear();
                }
                else if (operators[i] == '*')
                {
                    result += numbers.Aggregate(1L, (accumulator, value) => accumulator * value);
                    numbers.Clear();
                }
            }

            return result;
        }
    }
}
