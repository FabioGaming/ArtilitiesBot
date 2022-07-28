using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;


namespace ArtilitiesBot.Events
{
    class messageEvent
    {

        public async Task onMessageHandler(SocketMessage message)
        {
            Utils.APIManager API = new Utils.APIManager();
            _ = Task.Run(async () =>
            {
                if((message.Author.IsBot || message.Author.IsWebhook))
                {
                    return;
                }
                if(message.ToString().ToLower().StartsWith("art!"))
                {
                    string command = message.ToString().ToLower().Substring(message.ToString().ToLower().IndexOf("art!") + "art!".Length);
                    string[] commandargs = command.Split();
                    switch(commandargs[0].ToLower())
                    {
                        case "help":
                            Commands.helpCommand gethelp = new Commands.helpCommand();
                            await gethelp.getHelp(message);
                            break;
                        case "idea":
                            Commands.getIdea getidea = new Commands.getIdea();
                            await getidea.getIdeaHandler(message);
                            break;
                        case "challenge":
                            Commands.getChallenge getChallenge = new Commands.getChallenge();
                            await getChallenge.getChallengeHandler(message);
                            break;
                        case "getchallenge":
                            Commands.startChallenge challenge = new Commands.startChallenge();
                            await challenge.startChallengeHandler(message);
                            break;
                        case "lookup":
                            Commands.getDictionary getEntry = new Commands.getDictionary();
                            await getEntry.getdictionaryEntry(message, command);
                            break;
                        default:
                            EmbedBuilder errormsg = new EmbedBuilder();
                            errormsg.Title = "Invalid Command.";
                            errormsg.Color = Color.Purple;
                            errormsg.Description = "The given Command does not exist, please make sure to provide a valid command to use the Artilities Bot!\nType **art!help** to get a list of commands.";
                            try
                            {
                                await message.Channel.SendMessageAsync("", false, errormsg.Build());
                            }catch{}
                            break;
                    }
                }
            });
        }

    }
}
