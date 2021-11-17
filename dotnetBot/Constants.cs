using System.Text.Json;
using discordBot.models;

namespace discordBot
{
    /// <summary>
    /// Holds constants to be referenced throughout the program from our configuration file.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The loaded appsettings.json file
        /// </summary>
        public static Configuration ConfigurationFile = null!;
        /// <summary>
        /// The url that we will post to for trello cards
        /// </summary>
        public static string TrelloPostCardUrl => $"https://api.trello.com/1/cards?key={ConfigurationFile.Trello.ApiKey}&token={ConfigurationFile.Trello.ApiToken}";
        /// <summary>
        /// The config file name that we will load from.
        /// </summary>
        private const string _configFileName = "appsettings.json";

        /// <summary>
        /// Load the configuration from the specified file
        /// </summary>
        /// <returns>Returns the configuration that is loaded</returns>
        /// <exception cref="InvalidOperationException">If it cannot deserialize, throw this exception</exception>
        public static async Task<Configuration> LoadConfigFromJson()
        {
            var fileData = await File.ReadAllTextAsync(_configFileName);
            return JsonSerializer.Deserialize<Configuration>(fileData) ?? throw new InvalidOperationException();

        }

        public static readonly HttpClient TrelloHttp = new();



    }
}
