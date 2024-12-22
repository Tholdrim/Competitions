namespace AdventOfCode2024
{
    [TestClass]
    public class Day22
    {
        [TestMethod]
        [DataRow("Sample 22 (1).txt", 37327623L, 24, DisplayName = "Sample 1")]
        [DataRow("Sample 22 (2).txt", 37990510L, 23, DisplayName = "Sample 2")]
        [DataRow("Input 22.txt", 14392541715L, 1628, DisplayName = "Input")]
        public void Solve(string fileName, long expectedResult1, int expectedResult2)
        {
            var result1 = 0L;
            var sequencePrices = new Dictionary<Sequence, int>();

            foreach (var line in File.ReadLines(fileName))
            {
                var secretNumber = int.Parse(line);

                var sequence = new Sequence(0, 0, 0, 0);
                var usedSequences = new HashSet<Sequence>();

                for (var i = 0; i < 2000; ++i)
                {
                    var nextSecretNumber = GenerateNextSecretNumber(secretNumber);
                    var price = nextSecretNumber % 10;

                    sequence = sequence.Shift(price - secretNumber % 10);
                    secretNumber = nextSecretNumber;

                    if (i >= 3 && usedSequences.Add(sequence))
                    {
                        sequencePrices[sequence] = sequencePrices.GetValueOrDefault(sequence) + price;
                    }
                }

                result1 += secretNumber;
            }

            var result2 = sequencePrices.Values.Max();

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static int GenerateNextSecretNumber(int secretNumber)
        {
            secretNumber = MixAndPrune(secretNumber, secretNumber * 64L);
            secretNumber = MixAndPrune(secretNumber, secretNumber / 32L);
            secretNumber = MixAndPrune(secretNumber, secretNumber * 2048L);

            return secretNumber;
        }

        private static int MixAndPrune(int a, long b) => (int)((a ^ b) % 16777216L);

        private record Sequence(int Delta1, int Delta2, int Delta3, int Delta4)
        {
            public Sequence Shift(int newDelta) => new(newDelta, Delta1, Delta2, Delta3);
        }
    }
}
