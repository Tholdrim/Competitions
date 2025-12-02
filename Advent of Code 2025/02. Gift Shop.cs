namespace AdventOfCode2025
{
    [TestClass]
    public class Day02
    {
        [TestMethod]
        [DataRow("Sample 02.txt", 1227775554L, 4174379265L, DisplayName = "Sample")]
        [DataRow("Input 02.txt", 44854383294L, 55647141923L, DisplayName = "Input")]
        public void Solve(string fileName, long expectedResult1, long expectedResult2)
        {
            var (result1, result2) = (0L, 0L);
            var input = File.ReadAllText(fileName);

            var ranges = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

            foreach (var range in ranges)
            {
                var ids = range.Split('-');
                var (lowerId, upperId) = (long.Parse(ids[0]), long.Parse(ids[1]));

                for (var id = lowerId; id <= upperId; ++id)
                {
                    var stringId = id.ToString();

                    for (var i = 2; i <= stringId.Length; ++i)
                    {
                        if (stringId.Length % i != 0)
                        {
                            continue;
                        }

                        var prefix = stringId[..(stringId.Length / i)];
                        var invalidId = IsInvalidId(stringId, prefix);

                        if (invalidId)
                        {
                            result1 += i == 2 ? id : 0L;
                            result2 += id;

                            break;
                        }
                    }
                }
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static bool IsInvalidId(string id, string prefix)
        {
            for (var j = prefix.Length; j < id.Length; j += prefix.Length)
            {
                if (id[j..(j + prefix.Length)] != prefix)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
