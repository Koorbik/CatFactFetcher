using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Netwise
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync("https://catfact.ninja/fact");

                var catFact = response.Trim();
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                var filePath = Path.Combine(desktopPath, "catfacts.txt");
                File.AppendAllText(filePath, catFact + Environment.NewLine);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}

