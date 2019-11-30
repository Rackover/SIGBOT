using SIGBOT.Components.War.Events;
using System;
using System.Collections.Generic;
using System.Text;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Components.War.Rules
{
    public class OneTakeRandomStreakCurse : Rule
    {
        public override void Advance(Teams teams, List<Region> regions, int step)
        {
            var teamPickList = new List<TEAM>();

            foreach (var team in teams)
            {
                foreach(var _ in team.territory)
                {
                    teamPickList.Add(team.id);
                }
            }

            var attacker = teamPickList[new Random().Next(teamPickList.Count)];
            var streak = 1f;
            while (true)
            {
                var team = teams[attacker];
                var targets = new List<Region>();
                foreach (var region in team.GetTerritory())
                {
                    foreach (var neighbor in region.neighbors)
                    {
                        if (Program.game.map.regions[neighbor].owner == team.id) continue;
                        targets.Add(Program.game.map.regions[neighbor]);
                    }
                }

                    if (targets.Count == 0) break; // No target

                var i = new Random().Next(targets.Count);
                var target = targets[i];

                if (// Random invasion fail chance
                    //new Random().Next(100) > 60 + team.territory.Count * 3
                    //|| 
                    // Streak fail
                    (streak > 1 && new Random().NextDouble() * streak * (20f / step) > team.territory.Count / 50f))
                {
                    //events.Add(new Repel() { defendedTerritory = target, repelled = team });
                    break; 
                }

                // Invasion success
                team.TakeOwnershipOf(target);

                streak++;
            }
            
        }
    }
}
