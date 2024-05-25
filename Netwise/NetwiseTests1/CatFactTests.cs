using Microsoft.VisualStudio.TestTools.UnitTesting;
using Netwise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine(catResponse?.ToString());
        }

        [TestMethod]
        public void AppendToFileTest()
        {
            var catFactWriter = new CatFactWriter();
            var catFact = new CatFact { Fact = "Cats are cool.", Length = 13 };
            var expectedContent = catFact.ToString();

            catFactWriter.AppendToFile(catFact);

            var fileContent = File.ReadAllText("catfact.txt");
            Assert.IsTrue(fileContent.Contains(expectedContent));
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists("catfact.txt"))
            {
                File.Delete("catfact.txt");
            }
        }
    }
}