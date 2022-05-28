using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtilitiesBot.Events
{
    class serverJoinEvent
    {

        public async Task onServerJoin(SocketGuild guild)
        {
            EmbedBuilder welcomeMSG = new EmbedBuilder();
            welcomeMSG.Color = Color.Purple;
            welcomeMSG.Title = "Thank you for Adding the Artilities Bot!";
            welcomeMSG.Description = "";
        }

    }
}
