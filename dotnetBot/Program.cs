using discordBot.messages;
using discordBot.typing;
using DSharpPlus;
using DSharpPlus.Entities;
using System.Text.Json;

MainAsync().GetAwaiter().GetResult();

static async Task MainAsync()
{
    Constants.ConfigurationFile = await Constants.LoadConfigFromJson();
    var discord = new DiscordClient(new DiscordConfiguration()
    {
        Token = Constants.ConfigurationFile.BotToken,
        TokenType = TokenType.Bot,
        Intents = DiscordIntents.All

    });

    var messageHandler = new MessageHandler(discord);
    var typingHandler = new TypingHandler(discord);
    messageHandler.ConfigureOnMessageEvents();
    typingHandler.SetupTypingEvents();
    discord.PresenceUpdated += async (sender, args) =>
    {
        if (args.User.Id == Constants.ConfigurationFile.KevinsUserId && args.PresenceBefore.Status == UserStatus.Online &&
            args.PresenceAfter.Status == UserStatus.Offline)
            await discord.SendMessageAsync(await discord.GetChannelAsync(Constants.ConfigurationFile.BotChannelId), "Our boss just signed out, lets party and eat pizza...");

    };
    await discord.ConnectAsync();
    Console.WriteLine("Connected successfully!");
    await Task.Delay(-1);
}

/// <summary>
/// Holds constants to be referenced throughout the program from our configuration file.
/// </summary>
public static class Constants
{
    public static Config ConfigurationFile = null!;
    private const string _configFileName = "appsettings.json";

    public static async Task<Config> LoadConfigFromJson()
    {
        var fileData = await File.ReadAllTextAsync(_configFileName);
        return JsonSerializer.Deserialize<Config>(fileData) ?? throw new InvalidOperationException();

    }


    /// <summary>
    /// This is a json mapping for the items so that it can be deserialized into the class directly.
    /// </summary>
    public class Config
    {
        public string BotToken { get; set; } = null!;
        public ulong KevinsUserId { get; set; }
        public ulong BotChannelId { get; set; }

    }

}


