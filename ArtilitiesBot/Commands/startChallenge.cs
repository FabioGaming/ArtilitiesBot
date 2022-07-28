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
            catch { }
        }
    }
}
