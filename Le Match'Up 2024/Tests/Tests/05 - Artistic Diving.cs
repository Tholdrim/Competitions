using LeMatchUp.Year2024.Task05;

namespace LeMatchUp.Year2024.Tests
{
    [TestClass]
    [TestCategory(Identifier)]
    public sealed class Task05 : TestBase<Task05.Parameters>
    {
        public const string Identifier = "05 - Artistic Diving";

        protected override void InvokeSolutionUnderTest() => Program.Main(default);

        public readonly struct Parameters : ITestParameters
        {
            public static string Identifier => Task05.Identifier;

            public static int Count { get; } = 9;
        }
    }
}
