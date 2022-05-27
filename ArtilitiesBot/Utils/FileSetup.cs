using System.IO;
using System.Threading.Tasks;

namespace ArtilitiesBot.Utils
{
    class FileSetup
    {
        public Task FileCheck()
        {
            if(Directory.Exists("properties"))
            {
                if(!File.Exists("properties/artilities.cfg")) { Setup(); }
                if(!File.Exists("properties/artilities.log")) { Setup(); }
            } else { Setup(); }
            return Task.CompletedTask;
        }
        private void Setup()
        {
            Directory.CreateDirectory("properties");
            using (StreamWriter sw = File.CreateText("properties/artilities.cfg")) { sw.WriteLine("botToken="); }
            File.CreateText("properties/artilities.log");
        }

    }
}
