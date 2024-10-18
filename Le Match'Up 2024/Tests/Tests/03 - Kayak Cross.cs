using LeMatchUp.Year2024.Task03;

namespace LeMatchUp.Year2024.Tests
{
    [TestClass]
    [TestCategory(Identifier)]
    public sealed class Task03 : TestBase<Task03.Parameters>
    {
        public const string Identifier = "03 - Kayak Cross";

        protected override void InvokeSolutionUnderTest() => Program.Main(default);

        public readonly struct Parameters : ITestParameters
        {
            public static string Identifier => Task03.Identifier;

            public static int Count { get; } = 10;
        }
    }
}
