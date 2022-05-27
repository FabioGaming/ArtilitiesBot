using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtilitiesBot
{
    class Program
    {
        public static DiscordSocketClient client;

        static async Task Main(string[] args)
        {
            Utils.APIManager API = new Utils.APIManager();
            Utils.FileSetup Setup = new Utils.FileSetup();
            Utils.valueClass valueReader = new Utils.valueClass();
            await Setup.FileCheck();
            await valueReader.readValues();


            var socketConfig = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers | GatewayIntents.GuildBans | GatewayIntents.All
            };
            client = new DiscordSocketClient(socketConfig);
            var token = Utils.valueClass.botToken;
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            //Events
            client.Log += logger;
        }



        private static async Task logger(LogMessage log)
        {
            await Utils.Logger.AddLog(log);
        }
    }
}
