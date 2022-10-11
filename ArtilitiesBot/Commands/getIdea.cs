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

            try
            {

                Utils.Templates.loadingEmbed loadingEmbedClass = new Utils.Templates.loadingEmbed();
                EmbedBuilder loadingEmbed = loadingEmbedClass._loadingEmbed;
                var origin = (IUserMessage)await message.Channel.SendMessageAsync("", false, loadingEmbed.Build());

                EmbedBuilder ideaMessage = new EmbedBuilder();
                Dictionary<string, string> idea = Artilities.main.GetIdea();
                if (idea != null)
                {
                    ideaMessage.Description = $"English: {idea["english"]}\n\nRussian: {idea["russian"]}";
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
                    await origin.ModifyAsync(msg => { msg.Embed = ideaMessage.Build(); });
                }
                catch (Exception e)
                {
                    await Utils.ErrorHandler.ErrorChecker(e, message, "getIdea");
                }
            }catch (Exception e) { await Utils.ErrorHandler.ErrorChecker(e, message, "getIdea"); }

        }
    }
}
