using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SIGBOT.Components.War.Events;

namespace SIGBOT.Components.War
{
    public class Game
    {
        public Rule rule;
        public static DateTime now;

        public Game(Rule rule)
        {
            this.rule = rule;
        }

        public void Run()
        {
            var display = new Display(scale: 10);
            var map = new Classroom.J002();

            for (var i = 1; i < 500; i++)
            {
                foreach (var team in map.teams.Keys.ToArray())
                {
                    if (map.teams[team].territory.Count <= 0)
                    {
                        map.teams.Remove(team);
                    }
                }
                now = DateTime.Now;
                display.WriteToDisk(
                    display.DrawMap(
                        map.regions,
                        map.teams.Values.ToList()
                    ),
                    "step" + i + ".png"
                );
                Console.WriteLine("Playing step " + i);


                if (map.teams.Count <= 1)
                {
                    var winner = map.teams.First().Value;
                    rule.events.Add(new Supremacy() { winner = winner, lastConquest = winner.territory.Last() });
                    Console.WriteLine(rule.events);
                    break;
                }

                rule.Advance(map.teams, map.regions, i);
            }
        }
    }
}
