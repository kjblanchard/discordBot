using System.Text.Json;
using discordBot.models;

namespace discordBot
{
    /// <summary>
    /// Holds constants to be referenced throughout the program from our configuration file.
    /// </summary>
    public static class Constants
    {
        public static Configuration ConfigurationFile = null!;
        public static string TrelloPostCardUrl => $"https://api.trello.com/1/cards?key={ConfigurationFile.Trello.ApiKey}&token={ConfigurationFile.Trello.ApiToken}";
        private const string _configFileName = "appsettings.json";

        public static async Task<Configuration> LoadConfigFromJson()
        {
            var fileData = await File.ReadAllTextAsync(_configFileName);
            return JsonSerializer.Deserialize<Configuration>(fileData) ?? throw new InvalidOperationException();

        }
        public static readonly HttpClient TrelloHttp = new();



    }
}
