using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace ArtilitiesBot.Utils
{
    public class ErrorHandler
    {
        public static async Task ErrorChecker(Exception e ,SocketMessage message, string source)
        {
            try
            {
                EmbedBuilder logMessage = new EmbedBuilder();
                logMessage.Title = "An Error Occurred";
                logMessage.Description = $"**Error Info**\n Script: {source}.cs\nError: {e}";
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

            try
            {
                EmbedBuilder noPerms = new EmbedBuilder();
                noPerms.Color = Color.Purple;
                noPerms.Title = "An Error Occurred";
                noPerms.Description = $"Unable to send messages in <#{message.Channel.Id}>\nPlease make sure this bot has the required permission to send / read messages";
                noPerms.Footer = new EmbedFooterBuilder
                {
                    Text = "Thank you for using Artilities!"
                };
                SocketGuild guild = (message.Channel as SocketGuildChannel).Guild;
                await guild.Owner.SendMessageAsync("", false, noPerms.Build());
            }
            catch
            {
                try
                {
                    EmbedBuilder noPerms = new EmbedBuilder();
                    noPerms.Color = Color.Purple;
                    noPerms.Title = "An Error Occurred";
                    noPerms.Description = $"Unable to send messages in <#{message.Channel.Id}>\nPlease make sure this bot has the required permission to send / read messages";
                    noPerms.Footer = new EmbedFooterBuilder
                    {
                        Text = "Thank you for using Artilities!"
                    };
                    SocketGuild guild = (message.Channel as SocketGuildChannel).Guild;
                    await guild.SystemChannel.SendMessageAsync("", false, noPerms.Build());
                }
                catch { }
            }
        }
    }
}
