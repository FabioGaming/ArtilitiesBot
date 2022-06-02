using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace ArtilitiesBot.Events
{
    class serverJoinEvent
    {

        public async Task onServerJoin(SocketGuild guild)
        {
            EmbedBuilder welcomeMSG = new EmbedBuilder();
            welcomeMSG.Color = Color.Purple;
            welcomeMSG.Title = "Thank you for Adding the Artilities to your server!";
            welcomeMSG.Description = "Thank you for adding the official Artilities Discord Bot!\nYou can use **art!help** to get a list of commands!";
            try
            {
                await guild.SystemChannel.SendMessageAsync("", false, welcomeMSG.Build());
            }catch{ }

        }

    }
}
