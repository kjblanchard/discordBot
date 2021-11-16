using System.Text.Json;
using discordBot.messages;
using discordBot.typing;
using DSharpPlus;
using DSharpPlus.Entities;

MainAsync().GetAwaiter().GetResult();

static async Task MainAsync()
{

    var configuration = await Constants.ReadConfigurationFromJson();
    if (configuration == null)
        throw new Exception("Did not create a proper config, do you know what you are doing?");
    Constants.ConfigFile = configuration;

    var discord = new DiscordClient(new DiscordConfiguration()
    {
        Token = "OTA4MDM0NDUzMzYyODYwMTQy.YYv2_A.yjQ1dQwxwdiQYNDSDQbZ1KJhapY",
        TokenType = TokenType.Bot,
        Intents = DiscordIntents.All

    });

    var messageHandler = new MessageHandler(discord);
    var typingHandler = new TypingHandler(discord);
    messageHandler.ConfigureOnMessageEvents();
    typingHandler.SetupTypingEvents();
    discord.PresenceUpdated += async (sender, args) =>
    {
        if (args.User.Id == Constants.ConfigFile.KevinsUserId && args.PresenceBefore.Status == UserStatus.Online &&
            args.PresenceAfter.Status == UserStatus.Offline)
            if (Constants.BotChannel is not null)
                await discord.SendMessageAsync(Constants.BotChannel, "Our boss just signed out, lets party and eat pizza...");

    };
    await discord.ConnectAsync();
    Console.WriteLine("Connected successfully!");
    await Task.Delay(-1);
}

public static class Constants
{

    public static DiscordChannel? BotChannel;
    public static Config ConfigFile = null!;

    private const string _configFileName = "appsettings.json";

    public static async Task<Config?> ReadConfigurationFromJson()
    {
        await using FileStream openFile = File.OpenRead(_configFileName);
        var jsonObj = JsonSerializer.DeserializeAsync<Config>(openFile);
        return jsonObj.Result;
    }

    public class Config
    {
        public string? BotToken;
        public ulong KevinsUserId;
        public ulong BotChannelId;
    }

}
