using Discord.WebSocket;
using System;
using Discord;
using System.Threading.Tasks;

namespace ArtilitiesBot.Events
{
    class ServerLeaveEvent
    {
        public async Task onServerLeave(SocketGuild guild)
        {

            try
            {
                EmbedBuilder logMessage = new EmbedBuilder();
                logMessage.Author = new EmbedAuthorBuilder
                {
                    IconUrl = guild.IconUrl,
                    Name = guild.Name
                };

                logMessage.Title = "Left Server";
                logMessage.Description = $"**Server Info**\nName: {guild.Name}\nID: {guild.Id}";
                logMessage.Color = Color.Purple;
                logMessage.Footer = new EmbedFooterBuilder()
                {
                    Text = $"{DateTime.Now.ToString("dd/MM/yyyy")} / {DateTime.Now.ToString("HH:mm:ss")}"
                };
                ITextChannel channel = (ITextChannel)Program.client.GetChannel(Convert.ToUInt64(Utils.valueClass.logChannel));
                await channel.SendMessageAsync("", false, logMessage.Build());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }




    }
}
