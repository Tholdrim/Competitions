using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatchUp2024.Tests
{
    [TestClass]
    public class Task06
    {

        [TestMethod]
        [DataRow("Data/Task 06/Input 01.txt", "Data/Task 06/Output 01.txt", DisplayName = "Input 01")]
        [DataRow("Data/Task 06/Input 02.txt", "Data/Task 06/Output 02.txt", DisplayName = "Input 02")]
        [DataRow("Data/Task 06/Input 03.txt", "Data/Task 06/Output 03.txt", DisplayName = "Input 03")]
        [DataRow("Data/Task 06/Input 04.txt", "Data/Task 06/Output 04.txt", DisplayName = "Input 04")]
        [DataRow("Data/Task 06/Input 05.txt", "Data/Task 06/Output 05.txt", DisplayName = "Input 05")]
        [DataRow("Data/Task 06/Input 06.txt", "Data/Task 06/Output 06.txt", DisplayName = "Input 06")]
        [DataRow("Data/Task 06/Input 07.txt", "Data/Task 06/Output 07.txt", DisplayName = "Input 07")]
        [DataRow("Data/Task 06/Input 08.txt", "Data/Task 06/Output 08.txt", DisplayName = "Input 08")]
        [DataRow("Data/Task 06/Input 09.txt", "Data/Task 06/Output 09.txt", DisplayName = "Input 09")]
        [DataRow("Data/Task 06/Input 10.txt", "Data/Task 06/Output 10.txt", DisplayName = "Input 10")]
        [DataRow("Data/Task 06/Input 11.txt", "Data/Task 06/Output 11.txt", DisplayName = "Input 11")]
        [DataRow("Data/Task 06/Input 12.txt", "Data/Task 06/Output 12.txt", DisplayName = "Input 12")]
        [DataRow("Data/Task 06/Input 13.txt", "Data/Task 06/Output 13.txt", DisplayName = "Input 13")]
        [DataRow("Data/Task 06/Input 14.txt", "Data/Task 06/Output 14.txt", DisplayName = "Input 14")]
        [DataRow("Data/Task 06/Input 15.txt", "Data/Task 06/Output 15.txt", DisplayName = "Input 15")]
        [DataRow("Data/Task 06/Input 16.txt", "Data/Task 06/Output 16.txt", DisplayName = "Input 16")]
        [DataRow("Data/Task 06/Input 17.txt", "Data/Task 06/Output 17.txt", DisplayName = "Input 17")]
        [DataRow("Data/Task 06/Input 18.txt", "Data/Task 06/Output 18.txt", DisplayName = "Input 18")]
        [DataRow("Data/Task 06/Input 19.txt", "Data/Task 06/Output 19.txt", DisplayName = "Input 19")]
        [DataRow("Data/Task 06/Input 20.txt", "Data/Task 06/Output 20.txt", DisplayName = "Input 20")]
        [DataRow("Data/Task 06/Input 21.txt", "Data/Task 06/Output 21.txt", DisplayName = "Input 21")]
        [DataRow("Data/Task 06/Input 22.txt", "Data/Task 06/Output 22.txt", DisplayName = "Input 22")]
        [DataRow("Data/Task 06/Input 23.txt", "Data/Task 06/Output 23.txt", DisplayName = "Input 23")]
        [DataRow("Data/Task 06/Input 24.txt", "Data/Task 06/Output 24.txt", DisplayName = "Input 24")]
        [DataRow("Data/Task 06/Input 25.txt", "Data/Task 06/Output 25.txt", DisplayName = "Input 25")]
        public void Test(string inputFileName, string outputFileName)
        {
            var expectedOutput = TestHelper.ReadFile(outputFileName);
            var output = TestHelper.ExecuteWithRedirectedIO(inputFileName, () => MatchUp2024.Task06.Program.Main(default));

            Assert.AreEqual(expectedOutput, output);
        }
    }
}
