using System;
using System.Collections.Generic;
using System.Text;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Components.War.Rules
{
    public class MultipleTryTakeOneChain : Rule
    {
        public override void Advance(Teams teams, List<Region> regions, int step)
        {
            foreach (var team in teams)
            {
                var targets = new List<Region>();
                foreach (var region in team.GetTerritory())
                {
                    foreach (var neighbor in region.neighbors)
                    {
                        if (team.territory.Contains(neighbor)) continue;
                        targets.Add(Program.game.map.regions[neighbor]);
                    }
                }

                if (targets.Count <= 0) continue;
                var streak = 1f;
                while (true)
                {
                    var i = new Random().Next(targets.Count);
                    Console.WriteLine(i);
                    var target = targets[i];

                    if (new Random().Next(100) > 90/streak) break; // Invasion failed; break streak

                    // Invasion success
                    team.TakeOwnershipOf(target);
                    streak += 0.6f;
                }
            }
        }
    }
}
