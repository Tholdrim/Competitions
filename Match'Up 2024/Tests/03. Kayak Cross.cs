using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatchUp2024.Tests
{
    [TestClass]
    public class Task03
    {
        [TestMethod]
        [DataRow("Data/Task 03/Input 01.txt", "3", DisplayName = "Input 01")]
        [DataRow("Data/Task 03/Input 02.txt", "4 6", DisplayName = "Input 02")]
        [DataRow("Data/Task 03/Input 03.txt", "4", DisplayName = "Input 03")]
        [DataRow("Data/Task 03/Input 04.txt", "8", DisplayName = "Input 04")]
        [DataRow("Data/Task 03/Input 05.txt", "2", DisplayName = "Input 05")]
        [DataRow("Data/Task 03/Input 06.txt", "1", DisplayName = "Input 06")]
        [DataRow("Data/Task 03/Input 07.txt", "25", DisplayName = "Input 07")]
        [DataRow("Data/Task 03/Input 08.txt", "16", DisplayName = "Input 08")]
        [DataRow("Data/Task 03/Input 09.txt", "1 6 11 16", DisplayName = "Input 09")]
        [DataRow("Data/Task 03/Input 10.txt", "6 13 20 27 34", DisplayName = "Input 10")]
        public void Test(string fileName, string expectedOutput)
        {
            var output = TestHelper.ExecuteWithRedirectedIO(fileName, () => MatchUp2024.Task03.Program.Main(default));

            Assert.AreEqual(expectedOutput, output);
        }
    }
}
