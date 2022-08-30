using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Net;
using Discord.WebSocket;

namespace ArtilitiesBot.Commands
{
    class getUser
    {




        public async Task getUserInfo(SocketMessage message, string command)
        {


            string[] commandargs = command.Split();
            string userID = commandargs[1];

            string target;
            List<SocketUser> userList = new List<SocketUser>();
            if(message.MentionedUsers.Count > 0)
            {
                foreach(SocketUser user in message.MentionedUsers)
                {
                    userList.Add(user);
                }
                target = userList[0].Id.ToString();
            } else
            {
                target = userID;
            }
            Dictionary<string, string> userIdeas = Artilities.users.getIdeas(target);
            Dictionary<string, string> userChallenges = Artilities.users.getChallenges(target);
            Dictionary<string, string> userColors = Artilities.users.getColors(target);
            if(userColors["statusCode"] == "200")
            {

                var targetUser = Program.client.Rest.GetUserAsync(Convert.ToUInt64(target));
                //Ideas
                EmbedBuilder user_ideas = new EmbedBuilder();
                user_ideas.Color = Color.Purple;
                user_ideas.AddField("User Information", $"{targetUser.Result.Username}#{targetUser.Result.Discriminator} [{targetUser.Result.Id}]");
                user_ideas.Title = $"Saved ideas of **{targetUser.Result.Username}**";
                user_ideas.Footer = new EmbedFooterBuilder()
                {
                    Text = $"Server responded with {userColors["statusCode"]} in {userColors["delayTime"]}ms"
                };
                string ideaDesc = "";
                string[] ideas = userIdeas["ideas"].Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                if(ideas.Length > 0)
                {
                    foreach(string idea in ideas)
                    {
                        ideaDesc = ideaDesc + "> " + idea + "\n";
                    }
                } else
                {
                    ideaDesc = "This user has no saved ideas.";
                }
                user_ideas.Description = ideaDesc;

                /*
                //Challenges
                EmbedBuilder user_challenges = new EmbedBuilder();
                user_challenges.Title = $"Saved Challenges of **{targetUser.Result.Username}**";
                user_challenges.Color = Color.Purple;
                string challengeDesc = "";
                string[] challenges = userChallenges["challenges"].Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                user_challenges.Footer = new EmbedFooterBuilder()
                {
                    Text = $"Server responded with {userColors["statusCode"]} in {userColors["delayTime"]}ms"
                };
                if (challenges.Length > 0)
                {
                    foreach(string challenge in challenges)
                    {
                        challengeDesc = challengeDesc + "> " + challenge + "\n";
                    }
                } else
                {
                    challengeDesc = "This user has no saved challenges.";
                }
                user_challenges.Description = challengeDesc;

                //Colors
                EmbedBuilder user_colors = new EmbedBuilder();
                user_colors.Color = Color.Purple;
                user_colors.Title = $"Saved Colors of **{targetUser.Result.Username}**";
                string colorDesc = "";
                string[] colors = userColors["colors"].Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                user_colors.Footer = new EmbedFooterBuilder()
                {
                    Text = $"Server responded with {userColors["statusCode"]} in {userColors["delayTime"]}ms"
                };
                if (colors.Length > 0)
                {
                    foreach(string color in colors)
                    {
                        colorDesc = colorDesc + "> " + color + "\n";
                    }
                } else
                {
                    colorDesc = "This user has no saved colors.";
                }
                user_colors.Description = colorDesc;

                */

                var getUserInfoMenu = new SelectMenuBuilder()
                    .WithPlaceholder("Saved Ideas")
                    .WithCustomId("user_information")
                    .WithMinValues(1)
                    .WithMaxValues(1)
                    .AddOption("Saved Ideas", "user_ideas", "See the users saved ideas!")
                    .AddOption("Saved Challenges", "user_challenges", "See the users saved challenges!")
                    .AddOption("Saved Colors", "user_colors", "See the users saved colors!");
                var getUserButtons = new ComponentBuilder()
                    .WithSelectMenu(getUserInfoMenu);


                //.WithButton("Previous", "getUser_Previous")
                //.WithButton("Next", "getUser_Next");
                try
                {
                    var msg = await message.Channel.SendMessageAsync("", false, user_ideas.Build(), components: getUserButtons.Build());
                    
                    
                    //await message.Channel.SendMessageAsync("Challenges", false, user_challenges.Build());
                    //await message.Channel.SendMessageAsync("Colors", false, user_colors.Build());
                }catch(Exception e)
                {
                    await sendError(e, message);
                }
            } else if(userColors["statusCode"] == "403")
            {
                EmbedBuilder user_isPrivate = new EmbedBuilder();
                user_isPrivate.Color = Color.Purple;
                user_isPrivate.Title = "This user is Private!";
                user_isPrivate.Description = $"The target users profile seems to be privated, privated members can't be looked up!\nTarget: {target}";
                user_isPrivate.Footer = new EmbedFooterBuilder()
                {
                    Text = $"Server responded with {userColors["statusCode"]} in {userColors["delayTime"]}ms"
                };

                try
                {
                    var msg = await message.Channel.SendMessageAsync("", false, user_isPrivate.Build());
                }catch(Exception e)
                {
                    await sendError(e, message);
                }
                

            } else if(userColors["statusCode"] == "404")
            {
                EmbedBuilder user_notFound = new EmbedBuilder();
                user_notFound.Color = Color.Purple;
                user_notFound.Title = "This user does not Exist!";
                user_notFound.Description = $"The user **{target}** does not have an Artilities Account / does not exist in the Artilities Database!";
                user_notFound.Footer = new EmbedFooterBuilder()
                {
                    Text = $"Server responded with {userColors["statusCode"]} in {userColors["delayTime"]}ms"
                };
                try
                {
                    await message.Channel.SendMessageAsync("", false, user_notFound.Build());
                }catch(Exception e)
                {
                    await sendError(e, message);
                }
            } else
            {
                EmbedBuilder user_Error = new EmbedBuilder();
                user_Error.Color = Color.Purple;
                user_Error.Title = "Something went wrong";
                user_Error.Description = "Something went wrong during the execution of this command!\nConsider talking to the Artilities Team if thís keeps happening.";
                user_Error.Footer = new EmbedFooterBuilder()
                {
                    Text = $"Server responded with {userColors["statusCode"]} in {userColors["delayTime"]}ms"
                };

                try
                {
                    await message.Channel.SendMessageAsync("", false, user_Error.Build());
                }catch(Exception e)
                {
                    await sendError(e, message);
                }
            }

        }
        

        public async Task updateEmbed(SocketMessageComponent embedMenu)
        {
            await embedMenu.UpdateAsync(msg => msg.Content = "Last modified by " + embedMenu.User.Mention);

            string ID = embedMenu.Message.Embeds.ElementAt(0).Fields.ElementAt(0).Value.ToString().Split().Last().Replace("[", null).Replace("]", null);

            switch(string.Join(", ", embedMenu.Data.Values))
            {
                case "user_ideas":
                    Dictionary<string, string> userIdeas = Artilities.users.getIdeas(ID);
                    if(userIdeas["statusCode"] == "200")
                    {
                        var targetUser = Program.client.Rest.GetUserAsync(Convert.ToUInt64(ID));

                        EmbedBuilder user_ideas = new EmbedBuilder();
                        user_ideas.Color = Color.Purple;
                        user_ideas.AddField("User Information", $"{targetUser.Result.Username}#{targetUser.Result.Discriminator} [{targetUser.Result.Id}]");
                        user_ideas.Title = $"Saved ideas of **{targetUser.Result.Username}**";
                        user_ideas.Footer = new EmbedFooterBuilder()
                        {
                            Text = $"Server responded with {userIdeas["statusCode"]} in {userIdeas["delayTime"]}ms"
                        };
                        string ideaDesc = "";
                        string[] ideas = userIdeas["ideas"].Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                        if (ideas.Length > 0)
                        {
                            foreach (string idea in ideas)
                            {
                                ideaDesc = ideaDesc + "> " + idea + "\n";
                            }
                        }
                        else
                        {
                            ideaDesc = "This user has no saved ideas.";
                        }
                        user_ideas.Description = ideaDesc;

                        await embedMenu.ModifyOriginalResponseAsync(msg => msg.Embed = user_ideas.Build());

                    } else if(userIdeas["statusCode"] == "403")
                    {
                        EmbedBuilder user_isPrivate = new EmbedBuilder();
                        user_isPrivate.Color = Color.Purple;
                        user_isPrivate.Title = "This user is Private!";
                        user_isPrivate.Description = $"The target users profile seems to be privated, privated members can't be looked up!\nTarget: {ID}";
                        user_isPrivate.Footer = new EmbedFooterBuilder()
                        {
                            Text = $"Server responded with {userIdeas["statusCode"]} in {userIdeas["delayTime"]}ms"
                        };
                        await embedMenu.ModifyOriginalResponseAsync(msg => msg.Embed = user_isPrivate.Build());
                    } else if(userIdeas["statusCode"] == "404")
                    {
                        EmbedBuilder user_notFound = new EmbedBuilder();
                        user_notFound.Color = Color.Purple;
                        user_notFound.Title = "This user does not Exist!";
                        user_notFound.Description = $"The user **{ID}** does not have an Artilities Account / does not exist in the Artilities Database!";
                        user_notFound.Footer = new EmbedFooterBuilder()
                        {
                            Text = $"Server responded with {userIdeas["statusCode"]} in {userIdeas["delayTime"]}ms"
                        };
                        await embedMenu.ModifyOriginalResponseAsync(msg => msg.Embed = user_notFound.Build());
                    } else
                    {
                        EmbedBuilder user_Error = new EmbedBuilder();
                        user_Error.Color = Color.Purple;
                        user_Error.Title = "Something went wrong";
                        user_Error.Description = "Something went wrong during the execution of this command!\nConsider talking to the Artilities Team if this keeps happening.";
                        user_Error.Footer = new EmbedFooterBuilder()
                        {
                            Text = $"Server responded with {userIdeas["statusCode"]} in {userIdeas["delayTime"]}ms"
                        };
                        await embedMenu.ModifyOriginalResponseAsync(msg => msg.Embed = user_Error.Build());
                    }
                    break;
                case "user_challenges":
                    Dictionary<string, string> userChallenges = Artilities.users.getIdeas(ID);
                    if (userChallenges["statusCode"] == "200")
                    {
                        var targetUser = Program.client.Rest.GetUserAsync(Convert.ToUInt64(ID));

                        EmbedBuilder user_challenges = new EmbedBuilder();
                        user_challenges.Color = Color.Purple;
                        user_challenges.AddField("User Information", $"{targetUser.Result.Username}#{targetUser.Result.Discriminator} [{targetUser.Result.Id}]");
                        user_challenges.Title = $"Saved Challenges of **{targetUser.Result.Username}**";
                        user_challenges.Footer = new EmbedFooterBuilder()
                        {
                            Text = $"Server responded with {userChallenges["statusCode"]} in {userChallenges["delayTime"]}ms"
                        };
                        string challengeDesc = "";
                        string[] challenges = userChallenges["challenges"].Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                        if (challenges.Length > 0)
                        {
                            foreach (string challenge in challenges)
                            {
                                challengeDesc = challengeDesc + "> " + challenge + "\n";
                            }
                        }
                        else
                        {
                            challengeDesc = "This user has no saved ideas.";
                        }
                        user_challenges.Description = challengeDesc;
                        await embedMenu.ModifyOriginalResponseAsync(msg => msg.Embed = user_challenges.Build());
                    }
                    else if (userChallenges["statusCode"] == "403")
                    {
                        EmbedBuilder user_isPrivate = new EmbedBuilder();
                        user_isPrivate.Color = Color.Purple;
                        user_isPrivate.Title = "This user is Private!";
                        user_isPrivate.Description = $"The target users profile seems to be privated, privated members can't be looked up!\nTarget: {ID}";
                        user_isPrivate.Footer = new EmbedFooterBuilder()
                        {
                            Text = $"Server responded with {userChallenges["statusCode"]} in {userChallenges["delayTime"]}ms"
                        };
                        await embedMenu.ModifyOriginalResponseAsync(msg => msg.Embed = user_isPrivate.Build());
                    }
                    else if (userChallenges["statusCode"] == "404")
                    {
                        EmbedBuilder user_notFound = new EmbedBuilder();
                        user_notFound.Color = Color.Purple;
                        user_notFound.Title = "This user does not Exist!";
                        user_notFound.Description = $"The user **{ID}** does not have an Artilities Account / does not exist in the Artilities Database!";
                        user_notFound.Footer = new EmbedFooterBuilder()
                        {
                            Text = $"Server responded with {userChallenges["statusCode"]} in {userChallenges["delayTime"]}ms"
                        };
                        await embedMenu.ModifyOriginalResponseAsync(msg => msg.Embed = user_notFound.Build());
                    }
                    else
                    {
                        EmbedBuilder user_Error = new EmbedBuilder();
                        user_Error.Color = Color.Purple;
                        user_Error.Title = "Something went wrong";
                        user_Error.Description = "Something went wrong during the execution of this command!\nConsider talking to the Artilities Team if this keeps happening.";
                        user_Error.Footer = new EmbedFooterBuilder()
                        {
                            Text = $"Server responded with {userChallenges["statusCode"]} in {userChallenges["delayTime"]}ms"
                        };
                        await embedMenu.ModifyOriginalResponseAsync(msg => msg.Embed = user_Error.Build());
                    }
                    break;
                case "user_colors":
                    Dictionary<string, string> userColors = Artilities.users.getIdeas(ID);
                    if (userColors["statusCode"] == "200")
                    {
                        var targetUser = Program.client.Rest.GetUserAsync(Convert.ToUInt64(ID));

                        EmbedBuilder user_colors = new EmbedBuilder();
                        user_colors.Color = Color.Purple;
                        user_colors.AddField("User Information", $"{targetUser.Result.Username}#{targetUser.Result.Discriminator} [{targetUser.Result.Id}]");
                        user_colors.Title = $"Saved colors of **{targetUser.Result.Username}**";
                        user_colors.Footer = new EmbedFooterBuilder()
                        {
                            Text = $"Server responded with {userColors["statusCode"]} in {userColors["delayTime"]}ms"
                        };
                        string colorDesc = "";
                        string[] colors = userColors["clors"].Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                        if (colors.Length > 0)
                        {
                            foreach (string color in colors)
                            {
                                colorDesc = colorDesc + "> " + color + "\n";
                            }
                        }
                        else
                        {
                            colorDesc = "This user has no saved ideas.";
                        }
                        user_colors.Description = colorDesc;
                        await embedMenu.ModifyOriginalResponseAsync(msg => msg.Embed = user_colors.Build());
                    }
                    else if (userColors["statusCode"] == "403")
                    {
                        EmbedBuilder user_isPrivate = new EmbedBuilder();
                        user_isPrivate.Color = Color.Purple;
                        user_isPrivate.Title = "This user is Private!";
                        user_isPrivate.Description = $"The target users profile seems to be privated, privated members can't be looked up!\nTarget: {ID}";
                        user_isPrivate.Footer = new EmbedFooterBuilder()
                        {
                            Text = $"Server responded with {userColors["statusCode"]} in {userColors["delayTime"]}ms"
                        };
                        await embedMenu.ModifyOriginalResponseAsync(msg => msg.Embed = user_isPrivate.Build());
                    }
                    else if (userColors["statusCode"] == "404")
                    {
                        EmbedBuilder user_notFound = new EmbedBuilder();
                        user_notFound.Color = Color.Purple;
                        user_notFound.Title = "This user does not Exist!";
                        user_notFound.Description = $"The user **{ID}** does not have an Artilities Account / does not exist in the Artilities Database!";
                        user_notFound.Footer = new EmbedFooterBuilder()
                        {
                            Text = $"Server responded with {userColors["statusCode"]} in {userColors["delayTime"]}ms"
                        };
                        await embedMenu.ModifyOriginalResponseAsync(msg => msg.Embed = user_notFound.Build());
                        
                    }
                    else
                    {
                        EmbedBuilder user_Error = new EmbedBuilder();
                        user_Error.Color = Color.Purple;
                        user_Error.Title = "Something went wrong";
                        user_Error.Description = "Something went wrong during the execution of this command!\nConsider talking to the Artilities Team if this keeps happening.";
                        user_Error.Footer = new EmbedFooterBuilder()
                        {
                            Text = $"Server responded with {userColors["statusCode"]} in {userColors["delayTime"]}ms"
                        };
                        await embedMenu.ModifyOriginalResponseAsync(msg => msg.Embed = user_Error.Build());

                    }
                    break;
            }

        }


        private static async Task sendError(Exception e, SocketMessage message)
        {
            try
            {
                EmbedBuilder logMessage = new EmbedBuilder();
                logMessage.Title = "An Error Occurred";
                logMessage.Description = $"**Error Info**\n Script: getUser.cs\nError: {e}";
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
