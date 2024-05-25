using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("NetwiseTests")]

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

    interface ICatFactFetcher
    {
         Task<CatFact?> GetCatFactAsync(CatFact? catFact);
    }

    internal class CatFactFetcher : ICatFactFetcher
    {
        public async Task<CatFact?> GetCatFactAsync(CatFact? catFact)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://catfact.ninja");
                client.DefaultRequestHeaders.Add("User-Agent", "Anything");

                var response = await client.GetAsync("https://catfact.ninja/fact");
                response.EnsureSuccessStatusCode();
                catFact = await response.Content.ReadFromJsonAsync<CatFact>();
            }
            
            return catFact;
        }
    }

    interface ICatFactWriter
    {
        void AppendToFile(CatFact? catFact);
    }

    internal class CatFactWriter : ICatFactWriter
    {
        public void AppendToFile(CatFact? catFact)
        {
            if (catFact != null)
            {
                File.AppendAllText("catfact.txt", catFact.ToString());
            }
            else 
            {
                Console.WriteLine("No cat fact found");
            }
        }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            CatFact? catFact = null;

            try
            {
                catFact = await new CatFactFetcher().GetCatFactAsync(catFact);
                new CatFactWriter().AppendToFile(catFact);
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HTTP Request failed: {httpEx.Message}");
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"File operation failed: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
