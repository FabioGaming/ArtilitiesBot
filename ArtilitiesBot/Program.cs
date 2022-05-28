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
            client.Ready += clientReady;
            client.JoinedGuild += onServerJoin;
            client.MessageReceived += onMessage;
            //Events end here
        }

        static async Task clientReady()
        {
            
        }


        
        private static async Task logger(LogMessage log)
        {
            await Utils.Logger.AddLog(log);
        }
        private static async Task onMessage(SocketMessage msg)
        {
            Events.messageEvent eventCall = new Events.messageEvent();
            await eventCall.onMessageHandler(msg);
        }
        private static async Task onServerJoin(SocketGuild guild)
        {
            Events.serverJoinEvent eventCall = new Events.serverJoinEvent();
            await eventCall.onServerJoin(guild);
        }
    }
}
