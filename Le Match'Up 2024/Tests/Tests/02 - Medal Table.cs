using LeMatchUp.Year2024.Task02;

namespace LeMatchUp.Year2024.Tests
{
    [TestClass]
    [TestCategory(Identifier)]
    public sealed class Task02 : TestBase<Task02.Parameters>
    {
        public const string Identifier = "02 - Medal Table";

        protected override void InvokeSolutionUnderTest() => Program.Main(default);

        public readonly struct Parameters : ITestParameters
        {
            public static string Identifier => Task02.Identifier;

            public static int Count { get; } = 9;
        }
    }
}
