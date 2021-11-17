using DSharpPlus;
using DSharpPlus.Entities;

namespace discordBot.Presence
{
    internal class PresenceMessageHandler
    {
        private readonly DiscordClient _discordClient;
        public PresenceMessageHandler(DiscordClient client)
        {
            _discordClient = client;
        }

        public void ConfigurePresenceEvents()
        {
            _discordClient.PresenceUpdated += async (sender, args) =>
                {
                    if (args.User.Id == Constants.ConfigurationFile.KevinsUserId && args.PresenceBefore.Status == UserStatus.Online &&
                        args.PresenceAfter.Status == UserStatus.Offline)
                        await _discordClient.SendMessageAsync(await _discordClient.GetChannelAsync(Constants.ConfigurationFile.BotChannelId), "Our boss just signed out, lets party and eat pizza...");

                };

        }
    }
}
