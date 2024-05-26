using System.Net.Http.Json;

namespace Netwise
{
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
}
