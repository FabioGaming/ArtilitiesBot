using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace ArtilitiesBot.Commands
{
    class getDictionary
    {
        public async Task getdictionaryEntry(SocketMessage message, string query)
        {


            try
            {
                Utils.Templates.loadingEmbed loadingEmbedClass = new Utils.Templates.loadingEmbed();
                EmbedBuilder loadingEmbed = loadingEmbedClass._loadingEmbed;
                var origin = (IUserMessage)await message.Channel.SendMessageAsync("", false, loadingEmbed.Build());


                string searchQuery = query.Substring(7);
                Dictionary<string, string> lookupOutput = Artilities.main.GetDictionaryEntry(searchQuery);
                EmbedBuilder responseEmbed = new EmbedBuilder();
                if (lookupOutput != null)
                {
                    responseEmbed.Footer = new EmbedFooterBuilder()
                    {
                        Text = $"Server responded with {lookupOutput["statusCode"]} in {lookupOutput["delayTime"]}MS"
                    };
                    responseEmbed.Color = Color.Purple;
                    if (lookupOutput["word"] != null)
                    {
                        responseEmbed.Title = "Your lookup result";
                        responseEmbed.Description = $"First best result for {searchQuery} is:\n\nTerm: {lookupOutput["word"]}\nDescription: {lookupOutput["description"]}";
                    }
                    else
                    {
                        responseEmbed.Title = "No result found.";
                        responseEmbed.Description = $"There was no result found for: {searchQuery}";
                    }
                }
                else
                {
                    responseEmbed.Title = "An error occurred!";
                    responseEmbed.Description = "The bot could not get a server response, if this issue continues, consider contacting the Artilities Team!";
                }
                try
                {
                    //await message.Channel.SendMessageAsync("", false, responseEmbed.Build());
                    await origin.ModifyAsync(msg => { msg.Embed = responseEmbed.Build(); });

                }
                catch (Exception e)
                {
                   await Utils.ErrorHandler.ErrorChecker(e, message, "getDictionary");
                }
            }
            catch (Exception e) { await Utils.ErrorHandler.ErrorChecker(e, message, "getDictionary"); }

        } 
    }
}
