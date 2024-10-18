using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatchUp2024.Tests
{
    [TestClass]
    public class Task04
    {

        [TestMethod]
        [DataRow("Data/Task 04/Input 01.txt", "Data/Task 04/Output 01.txt", DisplayName = "Input 01")]
        [DataRow("Data/Task 04/Input 02.txt", "Data/Task 04/Output 02.txt", DisplayName = "Input 02")]
        [DataRow("Data/Task 04/Input 03.txt", "Data/Task 04/Output 03.txt", DisplayName = "Input 03")]
        [DataRow("Data/Task 04/Input 04.txt", "Data/Task 04/Output 04.txt", DisplayName = "Input 04")]
        [DataRow("Data/Task 04/Input 05.txt", "Data/Task 04/Output 05.txt", DisplayName = "Input 05")]
        [DataRow("Data/Task 04/Input 06.txt", "Data/Task 04/Output 06.txt", DisplayName = "Input 06")]
        [DataRow("Data/Task 04/Input 07.txt", "Data/Task 04/Output 07.txt", DisplayName = "Input 07")]
        [DataRow("Data/Task 04/Input 08.txt", "Data/Task 04/Output 08.txt", DisplayName = "Input 08")]
        [DataRow("Data/Task 04/Input 09.txt", "Data/Task 04/Output 09.txt", DisplayName = "Input 09")]
        [DataRow("Data/Task 04/Input 10.txt", "Data/Task 04/Output 10.txt", DisplayName = "Input 10")]
        [DataRow("Data/Task 04/Input 11.txt", "Data/Task 04/Output 11.txt", DisplayName = "Input 11")]
        [DataRow("Data/Task 04/Input 12.txt", "Data/Task 04/Output 12.txt", DisplayName = "Input 12")]
        public void Test(string inputFileName, string outputFileName)
        {
            var expectedOutput = TestHelper.ReadFile(outputFileName);
            var output = TestHelper.ExecuteWithRedirectedIO(inputFileName, () => MatchUp2024.Task04.Program.Main(default));

            Assert.AreEqual(expectedOutput, output);
        }
    }
}
