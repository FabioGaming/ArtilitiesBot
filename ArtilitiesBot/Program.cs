using System;
using System.Collections.Generic;

namespace ArtilitiesBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Utils.APIManager API = new Utils.APIManager();

            Dictionary<string, string> response = API.GetDictionaryEntry("fav");
            Console.WriteLine(response["word"]);
        }
    }
}
