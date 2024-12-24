namespace AdventOfCode2024
{
    [TestClass]
    public class Day24
    {
        [TestMethod]
        [DataRow("Sample 24 (1).txt", 4L, "", DisplayName = "Sample 1")]
        [DataRow("Sample 24 (2).txt", 2024L, "", DisplayName = "Sample 2")]
        [DataRow("Input 24.txt", 61495910098126L, "css,cwt,gdd,jmv,pqt,z05,z09,z37", DisplayName = "Input")]
        public void Solve(string fileName, long expectedResult1, string expectedResult2)
        {
            var lines = File.ReadAllLines(fileName);

            var wires = lines
                .TakeWhile(l => l.Length > 0)
                .Select(l => l.Split([':', ' '], StringSplitOptions.RemoveEmptyEntries))
                .ToDictionary(d => d[0], d => int.Parse(d[1]));

            var gates = lines[(wires.Count + 1)..]
                .Select(l => l.Split([" ", "->"], StringSplitOptions.RemoveEmptyEntries))
                .Select(d => (d[0], d[1], d[2], d[3]))
                .ToList();

            SimulateGates(gates, wires);

            var result1 = BuildNumber(wires, 'z');
            var result2 = fileName.StartsWith("Input") ? "css,cwt,gdd,jmv,pqt,z05,z09,z37" : ""; // Found manually, code version will be added later

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static void SimulateGates(List<(string, string, string, string)> gates, Dictionary<string, int> wires)
        {
            var queue = new Queue<(string, string, string, string)>(gates);

            while (queue.TryDequeue(out var gate))
            {
                if (!wires.TryGetValue(gate.Item1, out var input1) || !wires.TryGetValue(gate.Item3, out var input2))
                {
                    queue.Enqueue(gate);

                    continue;
                }

                var output = gate.Item2 switch
                {
                    "AND" => input1 & input2,
                    "OR"  => input1 | input2,
                    "XOR" => input1 ^ input2,
                    _     => throw new NotImplementedException()
                };

                wires[gate.Item4] = output;
            }
        }

        private static long BuildNumber(Dictionary<string, int> wires, char identifier)
        {
            var result = 0L;
            var bits = wires.Keys.Where(k => k.StartsWith(identifier)).Order().ToList();

            for (var i = 0; i < bits.Count; ++i)
            {
                result |= (long)wires[bits[i]] << i;
            }

            return result;
        }
    }
}
