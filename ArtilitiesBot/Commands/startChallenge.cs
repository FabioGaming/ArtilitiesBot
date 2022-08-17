using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace ArtilitiesBot.Commands
{
    class startChallenge
    {
        public async Task startChallengeHandler(SocketMessage message)
        {
            //Utils.APIManager API = new Utils.APIManager();
            EmbedBuilder challengeBuilder = new EmbedBuilder();
            Dictionary<string, string> responseIdea = Artilities.main.GetIdea();
            Dictionary<string, string> responseChallenge = Artilities.main.GetChallenge();
            if ((responseIdea != null && responseChallenge != null))
            {
                challengeBuilder.Description = $"English:\nIdea: {responseIdea["english"]}\nChallenge: {responseChallenge["english"]}\n\nRussian:\nIdea: {responseIdea["russian"]}\nChallenge: {responseChallenge["russian"]}";
            }
            else { challengeBuilder.Description = "An Error occurred."; }
            challengeBuilder.Footer = new EmbedFooterBuilder()
            {
                Text = $"Server responded with {responseIdea["statusCode"]} in {responseIdea["delayTime"]}MS"
            };
            challengeBuilder.Color = Color.Purple;
            challengeBuilder.Title = "Random Drawing Challenge";
            try
            {
                await message.Channel.SendMessageAsync("", false, challengeBuilder.Build());
            }
            catch(Exception e)
            {
                try
                {
                    EmbedBuilder logMessage = new EmbedBuilder();
                    logMessage.Title = "An Error Occurred";
                    logMessage.Description = $"**Error Info**\n Script: startChallenge.cs\nError: {e}";
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
}
