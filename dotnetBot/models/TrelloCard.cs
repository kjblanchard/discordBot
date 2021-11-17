namespace discordBot.models
{
    /// <summary>
    /// The model so that we can make json cards quick and easily
    /// </summary>
    internal class TrelloCard
    {
    /// <summary>
    /// The model so that we can make json cards quick and easily
    /// </summary>
        public TrelloCard()
        {
            
        }
        /// <summary>
        /// The description of the card
        /// </summary>
        public string desc { get; set; } = null!;
        public string idBoard { get; set; } = Constants.ConfigurationFile.Trello.JrpgBoardId;
        public string idList { get; set; } = Constants.ConfigurationFile.Trello.BacklogListId;
        /// <summary>
        /// The name of the card
        /// </summary>
        public string name { get; set; } = null!;
    }
}
