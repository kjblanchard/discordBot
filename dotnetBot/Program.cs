using discordBot.messages;
using discordBot.typing;
using DSharpPlus;
using DSharpPlus.Entities;

MainAsync().GetAwaiter().GetResult();

static async Task MainAsync()
{
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
        if (args.User.Id == Constants.KevinsUserId && args.PresenceBefore.Status == UserStatus.Online &&
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
    public const ulong KevinsUserId = 341314463619612673;
    public const ulong BotChannelId = 909534180784885821;
    public static DiscordChannel? BotChannel = null;

}
