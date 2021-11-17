

using System.Text;
using System.Text.Json;

namespace discordBot.messages
{
    internal class TrelloMessageHandler
    {
        public async Task<string> CreateCards(string informationPassedIn)
        {
            var nameAndDesc = informationPassedIn.Split(',', 2);
            if (nameAndDesc.Length != 2)
                return "This command is used like !addbacklog name,description";
            using var client = Constants.TrelloHttp;
            var jsonBody = new models.TrelloCard
            {
                name = nameAndDesc[0].Trim(),
                desc = nameAndDesc[1].Trim(),
            };
            var jsonString = JsonSerializer.Serialize(jsonBody);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(Constants.TrelloPostCardUrl, content);
            Console.WriteLine(result.StatusCode);

            return "Just made that for you boss!";
        }
    }
}
