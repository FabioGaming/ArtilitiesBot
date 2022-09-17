using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ArtilitiesBot.Utils
{
    class valueClass
    {
        public static string botToken;
        public static string logChannel;
        public static string devKey;
        public static string userID;

        public static string version = "1.1.4";

        //Reads the values from the artilities Bot Config
        public Task readValues()
        {
            Console.WriteLine("Reading config");
            var config = new Dictionary<string, string>();
            foreach (var row in File.ReadAllLines("properties/artilities.cfg"))
            {
                config.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()));
            }
            botToken = config["botToken"];
            logChannel = config["logChannel"];
            devKey = config["devKey"];
            userID = config["userID"];

            //Will prompt you to enter botToken and logChannel if the values are empty on startup
            if(string.IsNullOrEmpty(botToken))
            {
                Console.WriteLine("botToken value is empty, please enter a Token");
                botToken = Console.ReadLine();
                updateConfig();

            }
            if(string.IsNullOrEmpty(logChannel))
            {
                Console.WriteLine("logChannel value is empty, please enter a Channel ID");
                logChannel = Console.ReadLine();
                updateConfig();
            }
            if(string.IsNullOrEmpty(devKey))
            {
                Console.WriteLine("devKey value is empty, please enter a DevKey!");
                devKey = Console.ReadLine();
                updateConfig();

            }
            if(string.IsNullOrEmpty(userID))
            {
                Console.WriteLine("UserID value is empty, please provide a userID!");
                userID = Console.ReadLine();
                updateConfig();
            }
            Console.WriteLine("Done reading Config");

            return Task.CompletedTask;
        }

        private Task updateConfig()
        {

            using (StreamWriter sw = File.CreateText("properties/artilities.cfg"))
            {
                sw.WriteLine("botToken=" + botToken);
                sw.WriteLine("logChannel=" + logChannel);
                sw.WriteLine("devKey=" + devKey);
                sw.WriteLine("userID=" + userID);
            }

                return Task.CompletedTask;
        }
    }
}
