using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatchUp2024.Tests
{
    [TestClass]
    public class Task01
    {
        [TestMethod]
        [DataRow("Data/Task 01/Input 01.txt", "4245555555555555", DisplayName = "Input 01")]
        [DataRow("Data/Task 01/Input 02.txt", "4299999996", DisplayName = "Input 02")]
        [DataRow("Data/Task 01/Input 03.txt", "42058252228397044314", DisplayName = "Input 03")]
        [DataRow("Data/Task 01/Input 04.txt", "42183125072085654093", DisplayName = "Input 04")]
        [DataRow("Data/Task 01/Input 05.txt", "4244050544060420618406240", DisplayName = "Input 05")]
        [DataRow("Data/Task 01/Input 06.txt", "4236720607033703040080460", DisplayName = "Input 06")]
        [DataRow("Data/Task 01/Input 07.txt", "422006616041064667608", DisplayName = "Input 07")]
        [DataRow("Data/Task 01/Input 08.txt", "4238430872163017051321121", DisplayName = "Input 08")]
        [DataRow("Data/Task 01/Input 09.txt", "42256880760018101187", DisplayName = "Input 09")]
        [DataRow("Data/Task 01/Input 10.txt", "42080580860673513162", DisplayName = "Input 10")]
        [DataRow("Data/Task 01/Input 11.txt", "42016003810736635677", DisplayName = "Input 11")]
        public void Test(string fileName, string expectedOutput)
        {
            var output = TestHelper.ExecuteWithRedirectedIO(fileName, () => MatchUp2024.Task01.Program.Main(default));

            Assert.AreEqual(expectedOutput, output);
        }
    }
}
