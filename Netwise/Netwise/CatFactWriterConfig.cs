namespace Netwise
{
    internal class CatFactWriterConfig
    {
        public string FileName { get; set; }
        public CatFactWriterConfig(string fileName = "catfact.txt")
        {
            FileName = fileName;
        }
    }
}
