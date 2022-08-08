using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace ArtilitiesBot.Commands
{
    class getChallenge
    {

        public async Task getChallengeHandler(SocketMessage message)
        {
            //Utils.APIManager API = new Utils.APIManager();
            EmbedBuilder challengeMessage = new EmbedBuilder();
            Dictionary<string, string> challenge = Artilities.main.GetChallenge();
            if (challenge != null)
            {
                challengeMessage.Description = $"English:{challenge["english"]}\n\nRussian: {challenge["russian"]}";
            }
            else { challengeMessage.Description = "An Error occurred."; }
            challengeMessage.Color = Color.Purple;
            challengeMessage.Title = "Random Art Challenge";
            challengeMessage.Footer = new EmbedFooterBuilder()
            {
                Text = $"Server responded with {challenge["statusCode"]} in {challenge["delayTime"]}MS"
            };
            try
            {
                await message.Channel.SendMessageAsync("", false, challengeMessage.Build());
            }
            catch(Exception e)
            {
                try
                {
                    EmbedBuilder logMessage = new EmbedBuilder();
                    logMessage.Title = "An Error Occurred";
                    logMessage.Description = $"**Error Info**\n Script: getChallenge.cs\nError: {e}";
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
