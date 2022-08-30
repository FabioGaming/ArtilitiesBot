using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace ArtilitiesBot.Events
{
    class ButtonHandler
    {

        public async Task ButtonEvent(SocketMessageComponent button)
        {
            /*
            switch(button.Data.CustomId)
            {
                case "getUser_Previous":
                    switch(button.Message.Content.ToLower())
                    {
                        case "ideas":
                            break;
                        case "challenges":
                            break;
                        case "colors":
                            break;
                    }
                    break;
                case "getUser_Next":
                    await button.Channel.SendMessageAsync("gae");
                    break;
            } */
        } 
    }
}
