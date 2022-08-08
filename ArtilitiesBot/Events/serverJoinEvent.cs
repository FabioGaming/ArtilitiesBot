using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace ArtilitiesBot.Events
{
    class serverJoinEvent
    {

        public async Task onServerJoin(SocketGuild guild)
        {
            EmbedBuilder welcomeMSG = new EmbedBuilder();
            welcomeMSG.Color = Color.Purple;
            welcomeMSG.Title = "Thank you for Adding Artilities to your server!";
            welcomeMSG.Description = "Thank you for adding the official Artilities Discord Bot!\nYou can use **art!help** to get a list of commands!";
            welcomeMSG.Footer = new EmbedFooterBuilder()
            {
                Text = "Artilities - The Solution to artblock"
            };
            try
            {
                await guild.SystemChannel.SendMessageAsync("", false, welcomeMSG.Build());
            }catch
            {
                try
                {
                    await guild.Owner.SendMessageAsync("", false, welcomeMSG.Build());
                } catch { }
            }
            try
            {
                EmbedBuilder logMessage = new EmbedBuilder();
                logMessage.Author = new EmbedAuthorBuilder
                {
                    IconUrl = guild.IconUrl,
                    Name = guild.Name
                };
                
                logMessage.Title = "Joined Server";
                logMessage.Description = $"**Server Info**\nName: {guild.Name}\nID: {guild.Id}\nOwner ID: {guild.OwnerId}\nMembers: {guild.MemberCount}";
                logMessage.Color = Color.Purple;
                logMessage.Footer = new EmbedFooterBuilder()
                {
                    Text = $"{DateTime.Now.ToString("dd/MM/yyyy")} / {DateTime.Now.ToString("HH:mm:ss")}"
                };
                ITextChannel channel = (ITextChannel)Program.client.GetChannel(Convert.ToUInt64(Utils.valueClass.logChannel));
                await channel.SendMessageAsync("", false, logMessage.Build());
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }

            
            
            
        }

    }
}
