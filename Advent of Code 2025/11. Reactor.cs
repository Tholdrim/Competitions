namespace AdventOfCode2025
{
    [TestClass]
    public class Day11
    {
        [TestMethod]
        [DataRow("Sample 11a.txt", 5, null, DisplayName = "Sample 1")]
        [DataRow("Sample 11b.txt", null, 2L, DisplayName = "Sample 2")]
        [DataRow("Input 11.txt", 749, 420257875695750L, DisplayName = "Input")]
        public void Solve(string fileName, int? expectedResult1, long? expectedResult2)
        {
            var devices = File.ReadAllLines(fileName).Select(ParseLine).ToDictionary(p => p.Key, p => p.Value);

            var result1 = expectedResult1.HasValue ? CalculatePart1(devices) : (int?)null;
            var result2 = expectedResult2.HasValue ? CalculatePart2("svr", devices, [], false, false) : (long?)null;

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static int CalculatePart1(Dictionary<string, string[]> devices)
        {
            var result = 0;
            var stack = new Stack<string>(devices["you"]);

            while (stack.TryPop(out var device))
            {
                foreach (var connectedDevice in devices[device])
                {
                    if (connectedDevice == "out")
                    {
                        ++result;

                        continue;
                    }

                    stack.Push(connectedDevice);
                }
            }

            return result;
        }

        private static long CalculatePart2(string device, Dictionary<string, string[]> devices, Dictionary<(string, bool, bool), long> cache, bool dacVisited, bool fftVisited)
        {
            var result = 0L;

            if (device == "out")
            {
                return dacVisited && fftVisited ? 1L : 0L;
            }

            if (cache.TryGetValue((device, dacVisited, fftVisited), out var value))
            {
                return value;
            }

            dacVisited |= device == "dac";
            fftVisited |= device == "fft";

            foreach (var connectedDevice in devices[device])
            {
                var intermediateResult = CalculatePart2(connectedDevice, devices, cache, dacVisited, fftVisited);

                result += (cache[(connectedDevice, dacVisited, fftVisited)] = intermediateResult);
            }

            return result;
        }

        private static KeyValuePair<string, string[]> ParseLine(string line)
        {
            var data = line.Split([':', ' '], StringSplitOptions.RemoveEmptyEntries);

            return new(data[0], [.. data[1..]]);
        }
    }
}
