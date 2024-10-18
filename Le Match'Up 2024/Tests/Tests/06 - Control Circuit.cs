using LeMatchUp.Year2024.Task06;

namespace LeMatchUp.Year2024.Tests
{
    [TestClass]
    [TestCategory(Identifier)]
    public sealed class Task06 : TestBase<Task06.Parameters>
    {
        public const string Identifier = "06 - Control Circuit";

        protected override void InvokeSolutionUnderTest() => Program.Main(default);

        public readonly struct Parameters : ITestParameters
        {
            public static string Identifier => Task06.Identifier;

            public static int Count { get; } = 26;
        }
    }
}
