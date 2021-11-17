using DSharpPlus;
using DSharpPlus.EventArgs;

namespace discordBot.messages
{
    internal class MessageHandler
    {
        /// <summary>
        /// The command to say hello to the world.
        /// </summary>
        private const string _helloCommand = "hello";
        /// <summary>
        /// The command to add in a trello card.
        /// </summary>
        private const string _trelloAddBacklogCard = "addbacklog";
        /// <summary>
        /// What our bot commands should start with.
        /// </summary>
        private const char _botMessageIdentifier = '!';

        /// <summary>
        /// The discord client that we are connected with.
        /// </summary>
        private readonly DiscordClient _client;
        /// <summary>
        /// The handler for all Trello messages.
        /// </summary>
        private readonly TrelloMessageHandler _trelloMessageHandler = new();

        /// <summary>
        /// The handler that handles all messages received by the bot.
        /// </summary>
        /// <param name="client"></param>
        public MessageHandler(DiscordClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Configures all of the message events that we should handle, and what to do with them.
        /// </summary>
        public void ConfigureOnMessageEvents()
        {
            _client.MessageCreated += async (sender, args) =>
            {
                if (IsMessageFromBot(args))
                    return;
                var messageContent = args.Message.Content.ToLower().Trim();
                if (!IsMessageAValidBotCommand(messageContent))
                    return;
                var cleanedCommand = CleanBotCommand(messageContent);
                switch (cleanedCommand)
                {
                    case _helloCommand:
                        await args.Message.RespondAsync("Hello, world!");
                        break;
                    case _trelloAddBacklogCard:
                        cleanedCommand = messageContent.Split(cleanedCommand).Last();
                        var message = await _trelloMessageHandler.CreateCards(cleanedCommand);
                        await args.Message.RespondAsync(message);
                        break;
                    default:
                        await args.Message.RespondAsync("Why don't you try an actual command??");
                        break;
                }
            };
        }
        /// <summary>
        /// Checks to see if the message was posted by a bot.
        /// </summary>
        /// <param name="command">The command that was received</param>
        /// <returns>True if the message was from a bot</returns>
        private bool IsMessageFromBot(MessageCreateEventArgs command) => command.Author == _client.CurrentUser;
        /// <summary>
        /// Checks to see if the message received starts with our bot identifier and that it has more than 2 letters.
        /// </summary>
        /// <param name="messageContent">The content of the message</param>
        /// <returns>True if it starts with our message content and longer than 2 letters.</returns>
        private static bool IsMessageAValidBotCommand(string messageContent) => messageContent.StartsWith(_botMessageIdentifier) && messageContent.Length > 2;
        /// <summary>
        /// Removes the whitespace from the command and gets rid of special characters, and returns a new string.  Also only grabs the first part of the string before the whitespace.
        /// </summary>
        /// <param name="message">The actual message content</param>
        /// <returns>The command that the bot should try and match with.</returns>
        private static string CleanBotCommand(string message) => new(message.Split(" ").First().Where(letter => !char.IsWhiteSpace(letter) && char.IsLetterOrDigit(letter)).ToArray());
    }

}
