namespace discordBot.models
{
    internal class TrelloCard
    {
        public string desc { get; set; }
        public string idBoard { get; set; } = Constants.ConfigurationFile.Trello.JrpgBoardId;
        public string idList { get; set; } = Constants.ConfigurationFile.Trello.BacklogListId;
        public string name { get; set; }

    }
}
