namespace Netwise.Tests
{
    [TestClass]
    public class CatFactTests
    {
        [TestMethod]
        public async Task GetCatFactAsyncTest()
        {
            var catFactFetcher = new CatFactFetcher();
            CatFact? catFact = null;

            var catResponse = await catFactFetcher.GetCatFactAsync(catFact);
            Assert.IsNotNull(catResponse);
        }

        [TestMethod]
        public void AppendToFileTest()
        {
            var catFactWriter = new CatFactWriter();
            var catFact = new CatFact { Fact = "Cats are cool.", Length = 13 };
            var expectedContent = catFact.ToString();
            var consoleLogger = new ConsoleLogger();
            var tempFileName = Path.GetTempFileName();

            catFactWriter.AppendToFile(catFact, consoleLogger, tempFileName);

            var fileContent = File.ReadAllText(tempFileName);
            Assert.IsTrue(fileContent.Contains(expectedContent));
            if (File.Exists(tempFileName))
            {
                File.Delete(tempFileName);
            }
        }
    }
}