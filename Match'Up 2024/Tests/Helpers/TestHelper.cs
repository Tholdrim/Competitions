using System;
using System.IO;
using System.Text;

namespace MatchUp2024.Tests
{
    internal static class TestHelper
    {
        public static string ExecuteWithRedirectedIO(string inputFilePath, Action testAction)
        {
            var outputBuilder = new StringBuilder();

            using var inputStream = File.OpenRead(inputFilePath);
            using var consoleInputReader = new StreamReader(inputStream);
            using var consoleOutputWriter = new StringWriter(outputBuilder);

            Console.SetIn(consoleInputReader);
            Console.SetOut(consoleOutputWriter);

            testAction();

            return NormalizeString(outputBuilder.ToString());
        }

        public static string ReadFile(string outputFileName)
        {
            var content = File.ReadAllText(outputFileName);

            return NormalizeString(content);
        }

        private static string NormalizeString(string content) => content.Trim().ReplaceLineEndings("\n");
    }
}
