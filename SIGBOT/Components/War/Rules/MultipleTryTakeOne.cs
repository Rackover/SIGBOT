using System;
using System.Collections.Generic;
using System.Text;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Components.War.Rules
{
    public class MultipleTryTakeOne : Rule
    {
        public override void Advance(Dictionary<TEAM, Team> teams, List<Region> regions, int step)
        {
            foreach(var teamID in teams.Keys)
            {
                var team = teams[teamID];
                var targets = new List<Region>();
                foreach (var region in team.territory)
                {
                    foreach (var neighbor in region.neighbors)
                    {
                        if (neighbor.owner == team) continue;
                        targets.Add(neighbor);
                    }
                }

                var i = new Random().Next(targets.Count);
                Console.WriteLine(i);
                var target = targets[i];

                if (new Random().Next(100) > 90) continue; // Invasion failed

                // Invasion success
                team.TakeOwnershipOf(target);
            }
        }
    }
}
