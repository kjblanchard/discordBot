using DSharpPlus;

namespace discordBot.messages
{
    internal class MessageHandler
    {
        private enum CommandsEnum
        {
            Default,
            Hello,
            AddBacklogCardTrello,
        }

        private Dictionary<string, CommandsEnum> _commandsDictionary = new()
        {
            { string.Empty, default },
            { "hello", CommandsEnum.Hello },
            { "addbacklog", CommandsEnum.AddBacklogCardTrello }
        };

        private const char _botMessageIdentifier = '!';
        private readonly DiscordClient _client;
        private readonly TrelloMessageHandler _trelloMessageHandler = new();

        public MessageHandler(DiscordClient client)
        {
            _client = client;


        }

        public void ConfigureOnMessageEvents()
        {
            _client.MessageCreated += async (sender, args) =>
            {
                if (args.Author == _client.CurrentUser)
                    return;
                var messageContent = args.Message.Content.ToLower().Trim();
                if (!messageContent.StartsWith(_botMessageIdentifier) || messageContent.Length < 2)
                    return;
                var cleanedCommand = new string(messageContent.Split(" ").First().Where(letter => !char.IsWhiteSpace(letter) && char.IsLetterOrDigit(letter)).ToArray());
                if (!_commandsDictionary.TryGetValue(cleanedCommand, out var commandToRun))
                {
                    await args.Message.RespondAsync("That command doesn't exist!");
                    return;
                }
                switch (commandToRun)
                {
                    case CommandsEnum.Default:
                        await args.Message.RespondAsync("Why would you try a blank space after the explamation point?");
                        break;
                    case CommandsEnum.Hello:
                        await args.Message.RespondAsync("Hello, world!");
                        break;
                    case CommandsEnum.AddBacklogCardTrello:
                        cleanedCommand = messageContent.Split(cleanedCommand).Last();
                        var message = await _trelloMessageHandler.CreateCards(cleanedCommand);
                        await args.Message.RespondAsync(message);
                        break;

                    default:
                        await args.Message.RespondAsync("How did this even get hit");
                        break;
                }

            };

        }
    }
}
