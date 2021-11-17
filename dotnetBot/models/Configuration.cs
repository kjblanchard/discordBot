
namespace discordBot.models
{
    /// <summary>
    /// This is a json mapping for the items so that it can be deserialized into the class directly.
    /// </summary>
    public class Configuration
    {
        public string BotToken { get; set; } = null!;
        public ulong KevinsUserId { get; set; }
        public ulong BotChannelId { get; set; }

        public TrelloBase Trello { get; set; } = null!;

        public class TrelloBase
        {
            public string ApiKey { get; set; } = null!;
            public string ApiToken { get; set; } = null!;
            public string JrpgBoardId { get; set; } = null!;
            public string BacklogListId { get; set; } = null!;
        }
    }
}
