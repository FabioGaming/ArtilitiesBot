using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Discord;

namespace ArtilitiesBot.Utils
{
    class Logger
    {
        public static async Task AddLog(LogMessage log)
        {
            using (StreamWriter sw = File.CreateText("properites/artilities.log"))
            {
                foreach(string line in File.ReadAllLines("properties/artilities.log"))
                {
                    sw.WriteLine(line);
                }
                sw.WriteLine($"[{DateTime.Now.ToString("dd/MM/yyyy")} | {DateTime.Now.ToString("HH:mm:ss")}] {log.Message}");
            }


        }

    }
}
