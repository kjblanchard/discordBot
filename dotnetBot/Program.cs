using discordBot.messages;
using discordBot.typing;
using DSharpPlus;
using discordBot;
using discordBot.Presence;

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
    var presenceHandler = new PresenceMessageHandler(discord);
    messageHandler.ConfigureOnMessageEvents();
    typingHandler.SetupTypingEvents();
    presenceHandler.ConfigurePresenceEvents();
    await discord.ConnectAsync();
    Console.WriteLine("Connected successfully!");
    await Task.Delay(-1);
}



