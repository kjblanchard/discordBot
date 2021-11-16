using DSharpPlus;

namespace discordBot.typing
{
    internal class TypingHandler
    {
        private readonly DiscordClient _client;

        public TypingHandler(DiscordClient client)
        {
            _client = client;
        }

        public void SetupTypingEvents()
        {
            _client.TypingStarted += async (sender, args) =>
            {
                var nickname = args.Guild.GetMemberAsync(args.User.Id).Result.Nickname;
                if (args.User.Id == Constants.ConfigurationFile.KevinsUserId && args.Channel.Id == Constants.ConfigurationFile.BotChannelId)
                {
                    await _client.SendMessageAsync(args.Channel, $"Kevin, quit typing you lil loser");
                }

            };
        }
    }
}
