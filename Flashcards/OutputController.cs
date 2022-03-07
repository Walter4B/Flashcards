using Microsoft.Extensions.Configuration;

namespace Flashcards
{
    internal class OutputController
    {
        private readonly IConfiguration configuration = GetConfig();


        internal void DisplayMessage(string messageKey)
        {
            Console.WriteLine(configuration[messageKey]);
        }

        internal static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Flashcards/appsettings.json");
            return (builder.Build());
        }
    }
}
