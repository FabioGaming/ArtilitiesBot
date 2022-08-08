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
            string searchQuery = query.Substring(7);
            Dictionary<string, string> lookupOutput = Artilities.main.GetDictionaryEntry(searchQuery);
            EmbedBuilder responseEmbed = new EmbedBuilder();
            if(lookupOutput != null)
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
                } else
                {
                    responseEmbed.Title = "No result found.";
                    responseEmbed.Description = $"There was no result found for: {searchQuery}";
                }
            } else
            {
                responseEmbed.Title = "An error occurred!";
                responseEmbed.Description = "The bot could not get a server response, if this issue continues, consider contacting the Artilities Team!";
            }
            try
            {
                await message.Channel.SendMessageAsync("", false, responseEmbed.Build());
            }catch(Exception e)
            {
                try
                {
                    EmbedBuilder logMessage = new EmbedBuilder();
                    logMessage.Title = "An Error Occurred";
                    logMessage.Description = $"**Error Info**\n Script: getDictionary.cs\nError: {e}";
                    logMessage.Color = Color.Red;
                    logMessage.Footer = new EmbedFooterBuilder()
                    {
                        Text = $"{DateTime.Now.ToString("dd/MM/yyyy")} / {DateTime.Now.ToString("HH:mm:ss")}"
                    };
                    ITextChannel channel = (ITextChannel)Program.client.GetChannel(Convert.ToUInt64(Utils.valueClass.logChannel));
                    await channel.SendMessageAsync("", false, logMessage.Build());
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                }
            }
        } 
    }
}
