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
            //Utils.APIManager API = new Utils.APIManager();
            EmbedBuilder ideaMessage = new EmbedBuilder();
            Dictionary<string, string> idea = Artilities.main.GetIdea();
            if (idea != null)
            {
                ideaMessage.Description = $"English:{idea["english"]}\n\nRussian: {idea["russian"]}";
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
            catch(Exception e)
            {
                try
                {
                    EmbedBuilder logMessage = new EmbedBuilder();
                    logMessage.Title = "An Error Occurred";
                    logMessage.Description = $"**Error Info**\n Script: getIdea.cs\nError: {e}";
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
