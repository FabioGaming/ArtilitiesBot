using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArtilitiesBot.Utils
{
    class valueClass
    {
        public static string botToken;


        public Task readValues()
        {
            var config = new Dictionary<string, string>();
            foreach (var row in File.ReadAllLines("properties/config.cfg"))
            {
                config.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()));
            }
            botToken = config["botToken"];
            return Task.CompletedTask;
        }
    }
}
