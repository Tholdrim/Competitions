using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatchUp2024.Tests
{
    [TestClass]
    public class Task02
    {
        [TestMethod]
        [DataRow("Data/Task 02/Input 01.txt", "Japon", DisplayName = "Input 01")]
        [DataRow("Data/Task 02/Input 02.txt", "Canada", DisplayName = "Input 02")]
        [DataRow("Data/Task 02/Input 03.txt", "bQbpvJBYbCjVwmdGIgto", DisplayName = "Input 03")]
        [DataRow("Data/Task 02/Input 04.txt", "NLbMblB", DisplayName = "Input 04")]
        [DataRow("Data/Task 02/Input 05.txt", "YMkmpumjcyUFMdTm", DisplayName = "Input 05")]
        [DataRow("Data/Task 02/Input 06.txt", "KRJlTwLCAqCrNBQkDqCrm", DisplayName = "Input 06")]
        [DataRow("Data/Task 02/Input 07.txt", "UKpYgeESCgAQoo", DisplayName = "Input 07")]
        [DataRow("Data/Task 02/Input 08.txt", "IwjEjKNJSWRNHPM", DisplayName = "Input 08")]
        [DataRow("Data/Task 02/Input 09.txt", "uMqsZgEEjtU", DisplayName = "Input 09")]
        public void Test(string fileName, string expectedOutput)
        {
            var output = TestHelper.ExecuteWithRedirectedIO(fileName, () => MatchUp2024.Task02.Program.Main(default));

            Assert.AreEqual(expectedOutput, output);
        }
    }
}
