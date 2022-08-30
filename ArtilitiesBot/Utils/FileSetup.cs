using System.IO;
using System.Threading.Tasks;

namespace ArtilitiesBot.Utils
{
    class FileSetup
    {
        //Checks if all files exist, will call Setup() if not
        public Task FileCheck()
        {
            System.Console.WriteLine("Checking files");
            if(Directory.Exists("properties"))
            {
                if(!File.Exists("properties/artilities.cfg")) { Setup(); }
                if(!File.Exists("properties/artilities.log")) { Setup(); }
            } else { Setup(); }
            System.Console.WriteLine("All files found");
            return Task.CompletedTask;
        }
        //Creates the needed files
        private void Setup()
        {
            System.Console.WriteLine("Creating Files");
            Directory.CreateDirectory("properties");
            using (StreamWriter sw = File.CreateText("properties/artilities.cfg")) { 
                sw.WriteLine("botToken="); 
                sw.WriteLine("logChannel=");
                sw.WriteLine("devKey=");
                sw.WriteLine("userID=");
            }
            File.CreateText("properties/artilities.log");
            System.Console.WriteLine("Created Files");
        }

    }
}
