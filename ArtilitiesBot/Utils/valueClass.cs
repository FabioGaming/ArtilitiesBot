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

        public static string version = "1.1.0";

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

            //Will prompt you to enter botToken and logChannel if the values are empty on startup
            if(string.IsNullOrEmpty(botToken))
            {
                Console.WriteLine("botToken value is empty, please enter a Token");
                botToken = Console.ReadLine();
                using (StreamWriter sw = File.CreateText("properties/artilities.cfg")) { sw.WriteLine("botToken=" + botToken); sw.WriteLine("logChannel=" + logChannel); }
            }
            if(string.IsNullOrEmpty(logChannel))
            {
                Console.WriteLine("logChannel value is empty, please enter a Channel ID");
                logChannel = Console.ReadLine();
                using (StreamWriter sw = File.CreateText("properties/artilities.cfg")) { sw.WriteLine("botToken=" + botToken); sw.WriteLine("logChannel=" + logChannel); }
            }
            Console.WriteLine("Done reading Config");
            return Task.CompletedTask;
        }
    }
}
