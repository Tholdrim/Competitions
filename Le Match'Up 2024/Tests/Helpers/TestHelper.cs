using System.Reflection;

namespace LeMatchUp.Year2024.Tests
{
    internal static class TestHelper
    {
        public static string GetDisplayName(MethodInfo _, object[] data)
        {
            const string prefix = "Input ";

            if (data.Length < 1 || data[0] is not string inputFilePath)
            {
                throw new ArgumentException("The first data item must be the input file path.", nameof(data));
            }

            var inputFileName = Path.GetFileName(inputFilePath);

            return $"Test {inputFileName[prefix.Length..(prefix.Length + 2)]}";
        }

        public static IEnumerable<object[]> GetTestCases(string identifier, int count) => Enumerable.Range(1, count).Select(i => new[]
        {
            $"Data/{identifier}/Input {i:00}.secret.txt",
            $"Data/{identifier}/Output {i:00}.secret.txt"
        });
    }
}
