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
            helpMessage.Description = "Artilities Syntax: **art!**\n**art!help** - Get a list of commands\n**art!idea** - Returns an art Idea\n**art!challenge** - returns a fun drawing challenge\n**art!lookup** *<term>* - Look up artist slang, example 'OC'\nart!getchallenge - Returns an art idea along with a fun challenge";
            try
            {
                await message.Channel.SendMessageAsync("", false, helpMessage.Build());
            }
            catch { }
        }
    }
}
