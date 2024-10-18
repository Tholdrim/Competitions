using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatchUp2024.Tests
{
    [TestClass]
    public class Task05
    {

        [TestMethod]
        [DataRow("Data/Task 05/Input 01.txt", "9", DisplayName = "Input 01")]
        [DataRow("Data/Task 05/Input 02.txt", "37", DisplayName = "Input 02")]
        [DataRow("Data/Task 05/Input 03.txt", "212", DisplayName = "Input 03")]
        [DataRow("Data/Task 05/Input 04.txt", "110", DisplayName = "Input 04")]
        [DataRow("Data/Task 05/Input 05.txt", "12825", DisplayName = "Input 05")]
        [DataRow("Data/Task 05/Input 06.txt", "8371", DisplayName = "Input 06")]
        [DataRow("Data/Task 05/Input 07.txt", "22127", DisplayName = "Input 07")]
        [DataRow("Data/Task 05/Input 08.txt", "94256", DisplayName = "Input 08")]
        [DataRow("Data/Task 05/Input 09.txt", "10", DisplayName = "Input 09")]
        public void Test(string inputFileName, string expectedOutput)
        {
            var output = TestHelper.ExecuteWithRedirectedIO(inputFileName, () => MatchUp2024.Task05.Program.Main(default));

            Assert.AreEqual(expectedOutput, output);
        }
    }
}
