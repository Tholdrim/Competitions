using System.Text;

namespace LeMatchUp.Year2024.Tests
{
    public abstract class TestBase<TParameters> where TParameters : ITestParameters
    {
        private static IEnumerable<object[]> TestCases => TestHelper.GetTestCases(TParameters.Identifier, TParameters.Count);

        [DataTestMethod]
        [DynamicData(
            dynamicDataSourceName: nameof(TestCases),
            DynamicDataDisplayName = nameof(TestHelper.GetDisplayName),
            DynamicDataDisplayNameDeclaringType = typeof(TestHelper))]
        public virtual void SolutionShouldProduceExpectedOutput(string inputFilePath, string expectedOutputFilePath)
        {
            var expectedOutput = ReadExpectedOutputFromFile(expectedOutputFilePath);
            var actualOutput = RunSolutionAndCaptureOutput(inputFilePath);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        protected abstract void InvokeSolutionUnderTest();

        private string RunSolutionAndCaptureOutput(string inputFilePath)
        {
            var outputBuilder = new StringBuilder();

            var originalIn = Console.In;
            var originalOut = Console.Out;

            try
            {
                using var inputStream = File.OpenRead(inputFilePath);
                using var inputReader = new StreamReader(inputStream);
                using var outputWriter = new StringWriter(outputBuilder);

                Console.SetIn(inputReader);
                Console.SetOut(outputWriter);

                InvokeSolutionUnderTest();
            }
            finally
            {
                Console.SetIn(originalIn);
                Console.SetOut(originalOut);
            }

            return NormalizeOutput(outputBuilder.ToString());
        }

        private static string ReadExpectedOutputFromFile(string expectedOutputFilePath)
        {
            var content = File.ReadAllText(expectedOutputFilePath);

            return NormalizeOutput(content);
        }

        private static string NormalizeOutput(string output) => output.Trim().ReplaceLineEndings("\n");
    }
}
