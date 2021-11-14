using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (args.User.Id == Constants.KevinsUserId && args.Channel.Id == Constants.BotChannelId)
                    await _client.SendMessageAsync(args.Channel, $"Kevin, quit typing you lil loser");

            };
        }
    }
}
