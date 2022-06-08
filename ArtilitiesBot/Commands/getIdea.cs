using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace ArtilitiesBot.Commands
{
    class getIdea
    {
        public async Task getIdeaHandler(SocketMessage message)
        {
            Utils.APIManager API = new Utils.APIManager();
            EmbedBuilder ideaMessage = new EmbedBuilder();
            Dictionary<string, string> idea = API.GetIdea();
            if (idea != null)
            {
                ideaMessage.Description = $"English:{idea["english"]}\nRussian: {idea["russian"]}";
            }
            else { ideaMessage.Description = "An Error occurred."; }

            ideaMessage.Color = Color.Purple;
            ideaMessage.Title = "Random Art Idea";
            ideaMessage.Footer = new EmbedFooterBuilder()
            {
                Text = $"Server responded with {idea["statusCode"]} in {idea["delayTime"]}MS"
            };
            try
            {
                await message.Channel.SendMessageAsync("", false, ideaMessage.Build());
            }
            catch { }
        }
    }
}
