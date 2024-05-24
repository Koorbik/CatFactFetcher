using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Netwise
{
    internal class CatFact
    {
        public string Fact { get; set; } = string.Empty;
        public int Length { get; set; } = 0;

        public override string ToString()
        {
            return $"Fact: {Fact} Length: {Length}\n";
        }
    }

    internal class Program
    {
        private static void Main()
        {
            CatFact catFact;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://catfact.ninja");
                client.DefaultRequestHeaders.Add("User-Agent", "Anything");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("https://catfact.ninja/fact").Result;
                response.EnsureSuccessStatusCode();
                catFact = response.Content.ReadFromJsonAsync<CatFact>().Result;
            }
            
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = Path.Combine(desktopPath, "catfact.txt");
            if (catFact != null)
            {
            File.AppendAllText(filePath, catFact.ToString());
            }
            else 
            {
                Console.WriteLine("No cat fact found");
            }
        }
    }
}
