using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace ArtilitiesBot.Events
{
    class selectMenuEvent
    {
        public async Task menuHandler(SocketMessageComponent menu)
        {
            
            switch (menu.Data.CustomId)
            {
                case "user_information":
                    Commands.getUser getUser = new Commands.getUser();
                    await getUser.updateEmbed(menu);
                    break;
            }
        }
    }
}
