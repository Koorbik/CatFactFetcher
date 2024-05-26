
namespace Netwise
{
    interface ICatFactWriter
    {
        void AppendToFile(CatFact? catFact, ILogger consoleLogger, string filename);
    }
}
