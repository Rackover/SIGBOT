using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SIGBOT.Components.War.Events;
using Newtonsoft.Json;
using System.IO;
using DSharpPlus.Entities;

namespace SIGBOT.Components.War
{
    public class Game
    {
        public Journal events = new Journal();
        public Rule rule;

        public Map map;
        public Display display;
        public DiscordChannel channel;

        public readonly string directory = "out/warBot";

        public Game(Rule rule, bool load=false)
        {
            Program.game = this;
            this.rule = rule;
            
            display = new Display(scale: 10);

            if (load) {
                Log.Info("Reading from disk...");
                ReadFromDisk();
            }
            else {
                map = new Classroom.J002();
                WriteToDisk();
            }
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
            Log.Trace("Reading all text...");
            var data = File.ReadAllText(Path.Combine(directory, "map.json"));
            Log.Trace("Deserializing...");
            Map map;
            try
            {
                map = JsonConvert.DeserializeObject<Map>(data);
            }
            catch(Exception e)
            {
                Log.Err(e);
                throw new Exception(e.ToString());
            }
            Log.Trace("Done!");
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

            if (map.teams.Count <= 1)
            {
                display.WriteToDisk(
                    display.DrawMap(
                        map.regions,
                        map.teams.ToList()
                    ),
                    Path.Combine(directory, i + ".png")
                );
                var winner = map.teams.First();
                events.Add(new Supremacy() { winner = winner.id, lastConquest = winner.territory.Last() });
                return true;
            }

            rule.Advance(map.teams, map.regions, i);
            display.WriteToDisk(
                display.DrawMap(
                    map.regions,
                    map.teams.ToList()
                ),
                Path.Combine(directory, i + ".png")
            );
            map.step++;

            return false;
        }
    }
}
