using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameBasicsOpdracht.MVC.Model;
using Newtonsoft.Json;

namespace GameBasicsOpdracht.Json
{
    public class JsonReader
    {
        public List<Speler> LeesSpelers()
        {
            List<Speler> Playerlist = new List<Speler>();
            // Read the Players from a JSON file and put them in a list
            using (StreamReader reader = new StreamReader(@"Json\Players.json"))
            {
                string json = reader.ReadToEnd();
                Playerlist = JsonConvert.DeserializeObject<List<Speler>>(json).ToList<Speler>();
            }
            return Playerlist;
        }
    }
}
