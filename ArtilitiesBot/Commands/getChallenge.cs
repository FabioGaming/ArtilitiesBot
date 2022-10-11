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





            try
            {
                Utils.Templates.loadingEmbed loadingEmbedClass = new Utils.Templates.loadingEmbed();
                EmbedBuilder loadingEmbed = loadingEmbedClass._loadingEmbed;
                var origin = (IUserMessage)await message.Channel.SendMessageAsync("", false, loadingEmbed.Build());


                EmbedBuilder challengeMessage = new EmbedBuilder();
                Dictionary<string, string> challenge = Artilities.main.GetChallenge();
                if (challenge != null)
                {
                    challengeMessage.Description = $"English: {challenge["english"]}\n\nRussian: {challenge["russian"]}";
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
                    //await message.Channel.SendMessageAsync("", false, challengeMessage.Build());
                    await origin.ModifyAsync(msg => { msg.Embed = challengeMessage.Build(); });

                }
                catch (Exception e)
                {
                    await Utils.ErrorHandler.ErrorChecker(e, message, "getChallenge");
                }
            }catch(Exception e) { await Utils.ErrorHandler.ErrorChecker(e, message, "getChallenge"); }

        }
        

    }
}
