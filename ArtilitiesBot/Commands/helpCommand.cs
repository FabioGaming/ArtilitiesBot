using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace ArtilitiesBot.Commands
{
    class helpCommand
    {
        public async Task getHelp(SocketMessage message)
        {
            EmbedBuilder helpMessage = new EmbedBuilder();
            helpMessage.Color = Color.Purple;
            helpMessage.Title = "List of commands for Artilities";
            helpMessage.Description = "Artilities Syntax: **art!**\n**art!help** - Get a list of commands\n**art!idea** - Returns an art Idea\n**art!challenge** - returns a fun drawing challenge\n**art!lookup** *<term>* - Look up artist slang, example 'OC'\n**art!getchallenge** - Returns an art idea along with a fun challenge\n**art!user** <ID> / <@> - Returns a users saved ideas, challenges and colors";
            try
            {
                await message.Channel.SendMessageAsync("", false, helpMessage.Build());
            }
            catch(Exception e)
            {
                try
                {
                    EmbedBuilder logMessage = new EmbedBuilder();
                    logMessage.Title = "An Error Occurred";
                    logMessage.Description = $"**Error Info**\n Script: helpCommand.cs\nError: {e}";
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
                } catch
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
}
