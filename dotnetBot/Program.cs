using System.Diagnostics;
using discordBot.messages;
using discordBot.typing;
using DSharpPlus;

MainAsync().GetAwaiter().GetResult();

static async Task MainAsync()
{
    var discord = new DiscordClient(new DiscordConfiguration()
    {
        Token = "OTA4MDM0NDUzMzYyODYwMTQy.YYv2_A.yjQ1dQwxwdiQYNDSDQbZ1KJhapY",
        TokenType = TokenType.Bot,
        Intents = DiscordIntents.AllUnprivileged

    });

    var messageHandler = new MessageHandler(discord);
    var typingHandler = new TypingHandler(discord);
    messageHandler.ConfigureOnMessageEvents();
    typingHandler.SetupTypingEvents();
    await discord.ConnectAsync();
    Console.WriteLine("Connected successfully!");
    await Task.Delay(-1);
}

public static class Constants
{
    public const ulong KevinsUserId = 341314463619612673;
    public const ulong BotChannelId = 909534180784885821;

}
