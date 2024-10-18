using LeMatchUp.Year2024.Task01;

namespace LeMatchUp.Year2024.Tests
{
    [TestClass]
    [TestCategory(Identifier)]
    public sealed class Task01 : TestBase<Task01.Parameters>
    {
        public const string Identifier = "01 - Universal Badge";

        protected override void InvokeSolutionUnderTest() => Program.Main(default);

        public readonly struct Parameters : ITestParameters
        {
            public static string Identifier => Task01.Identifier;

            public static int Count { get; } = 10;
        }
    }
}
