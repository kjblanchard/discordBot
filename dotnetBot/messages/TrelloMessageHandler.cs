using System.Text;
using System.Text.Json;

namespace discordBot.messages
{
    /// <summary>
    /// This is the Handler for all trello messages.
    /// </summary>
    internal class TrelloMessageHandler
    {
        /// <summary>
        /// Creates the card on the correct board, and gives the bot a response to say after.
        /// </summary>
        /// <param name="informationPassedIn">The message content that we should look at</param>
        /// <returns>A string that will be sent by the bot after the request is made</returns>
        public async Task<string> CreateCards(string informationPassedIn)
        {
            var nameAndDesc = informationPassedIn.Split(',', 2);
            if (nameAndDesc.Length != 2)
                return "This command is used like !addbacklog name,description";
            using var httpClient = Constants.TrelloHttp;
            var jsonBody = new models.TrelloCard
            {
                name = nameAndDesc.First().Trim(),
                desc = nameAndDesc.Last().Trim(),
            };
            var jsonString = JsonSerializer.Serialize(jsonBody);
            var body = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(Constants.TrelloPostCardUrl, body);
            Console.WriteLine($"The status code of our response was {result.StatusCode}");
            return result.IsSuccessStatusCode ? "Just made that for you boss!" : $"Something went wrong, the result was {result.StatusCode}, check my log files!";
        }
    }
}
