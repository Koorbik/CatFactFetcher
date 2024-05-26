using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NetwiseTests")]

namespace Netwise
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddSingleton<ICatFactFetcher, CatFactFetcher>();
            builder.Services.AddSingleton<ICatFactWriter, CatFactWriter>();
            builder.Services.AddSingleton<ILogger, ConsoleLogger>();
            builder.Services.AddSingleton<CatFactWriterConfig>(new CatFactWriterConfig("catfact.txt"));

            using IHost host = builder.Build();

            await FetchAndWriteCatFactAsync(host);

            static async Task FetchAndWriteCatFactAsync(IHost host)
            {
                ICatFactFetcher catFactFetcher = host.Services.GetRequiredService<ICatFactFetcher>();
                ICatFactWriter catFactWriter = host.Services.GetRequiredService<ICatFactWriter>();
                ILogger consoleLogger = host.Services.GetRequiredService<ILogger>();
                CatFactWriterConfig filename = host.Services.GetRequiredService<CatFactWriterConfig>();

                CatFact? catFact = null;
                try
                {
                    catFact = await catFactFetcher.GetCatFactAsync(catFact);
                    catFactWriter.AppendToFile(catFact, consoleLogger, filename.FileName);
                }
                catch (HttpRequestException httpEx)
                {
                    consoleLogger.Log($"HTTP Request failed: {httpEx.Message}");
                }
                catch (IOException ioEx)
                {
                    consoleLogger.Log($"File operation failed: {ioEx.Message}");
                }
                catch (Exception ex)
                {
                    consoleLogger.Log($"An unexpected error occurred: {ex.Message}");
                }
            }
        }
    }
}
