using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SIGBOT.Components.War.Events;
using Newtonsoft.Json;
using System.IO;

namespace SIGBOT.Components.War
{
    public class Game
    {
        public Journal events = new Journal();
        public Rule rule;
        public DateTime now;

        public Map map;
        public Display display;

        public readonly string directory = "out/warBot";

        public Game(Rule rule)
        {
            Program.game = this;
            this.rule = rule;
            
            display = new Display(scale: 10);
            map = new Classroom.J002();
            WriteToDisk();
        }

        public void WriteToDisk()
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var data = JsonConvert.SerializeObject(map, Formatting.Indented);
            File.WriteAllText(Path.Combine(directory, "map.json"), data);
        }

        public void ReadFromDisk()
        {
            if (!File.Exists(Path.Combine(directory, "map.json"))) return;
            var data = File.ReadAllText(Path.Combine(directory, "map.json"));
            var map = JsonConvert.DeserializeObject<Map>(data);
            this.map = map;
        }

        public bool Advance()
        {
            var i = map.step;
            map.teams.Rearrange();
            map.regions.Rearrange();

            foreach (var team in map.teams.ToArray())
            {
                if (team.territory.Count <= 0)
                {
                    map.teams.Remove(team);
                }
            }
            now = DateTime.Now;
            display.WriteToDisk(
                display.DrawMap(
                    map.regions,
                    map.teams.ToList()
                ),
                Path.Combine(directory, i + ".png")
            );
            Console.WriteLine("Playing step " + i);


            if (map.teams.Count <= 1)
            {
                var winner = map.teams.First();
                events.Add(new Supremacy() { winner = winner.id, lastConquest = winner.territory.Last() });
                return true;
            }

            rule.Advance(map.teams, map.regions, i);
            map.step++;

            return false;
        }
    }
}
