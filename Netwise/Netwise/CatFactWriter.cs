
namespace Netwise
{
    internal class CatFactWriter : ICatFactWriter
    {
        public void AppendToFile(CatFact? catFact, ILogger consoleLogger, string filename)
        {
            if (catFact != null)
            {
                File.AppendAllText(filename, catFact.ToString());
            }
            else
            {
                consoleLogger.Log("No cat fact found");
            }
        }
    }
}
