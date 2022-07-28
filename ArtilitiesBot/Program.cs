using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

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



            await Task.Delay(-1);
        }

        //Handles mainUI for console
        static async Task clientReady()
        {
            //Updates rich presence every 10 seconds
            _ = Task.Run(async () => {
                while(true)
                {
                    try
                    {
                        await client.SetGameAsync("art! on " + client.Guilds.Count + " servers", "", ActivityType.Listening);
                    }catch{}
                    await Task.Delay(10000);
                }
            });
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Artilities Bot " + Utils.valueClass.version);
                Console.WriteLine("");
                Console.WriteLine("1: See servers");
                Console.WriteLine("quit: Stop the bot");
                string input = Console.ReadLine();
                switch(input.ToLower())
                {
                    case "1":
                        Console.WriteLine("");
                        Console.WriteLine("All Servers:");
                        Console.WriteLine("FORMAT: NAME, ID, OWNER_ID, MEMBERS");
                        foreach(SocketGuild guild in client.Guilds)
                        {
                            Console.WriteLine($"{guild.Name.PadRight(35)} | {guild.Id} | {guild.OwnerId} | {guild.MemberCount}");
                            Thread.Sleep(150);
                        }
                        Console.WriteLine("");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        break;
                    case "quit":
                        await client.LogoutAsync();
                        await client.StopAsync();
                        Environment.Exit(0);
                        break;
                }

            }
        }


        //Adds Discord Log Messages to Log file
        private static async Task logger(LogMessage log)
        {
            await Utils.Logger.AddLog(log);
        }

        //Handles Messages (Commands)
        private static async Task onMessage(SocketMessage msg)
        {
            Events.messageEvent eventCall = new Events.messageEvent();
            await eventCall.onMessageHandler(msg);
        }

        //Handles Server Join
        private static async Task onServerJoin(SocketGuild guild)
        {
            Events.serverJoinEvent eventCall = new Events.serverJoinEvent();
            await eventCall.onServerJoin(guild);
        }
    }
}
