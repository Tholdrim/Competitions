using LeMatchUp.Year2024.Task04;

namespace LeMatchUp.Year2024.Tests
{
    [TestClass]
    [TestCategory(Identifier)]
    public sealed class Task04 : TestBase<Task04.Parameters>
    {
        public const string Identifier = "04 - Climbing";

        protected override void InvokeSolutionUnderTest() => Program.Main(default);

        public readonly struct Parameters : ITestParameters
        {
            public static string Identifier => Task04.Identifier;

            public static int Count { get; } = 12;
        }
    }
}
