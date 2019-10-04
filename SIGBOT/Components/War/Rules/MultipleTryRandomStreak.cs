using SIGBOT.Components.War.Events;
using System;
using System.Collections.Generic;
using System.Text;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Components.War.Rules
{
    public class MultipleTryRandomStreak : Rule
    {
        public override void Advance(Dictionary<TEAM, Team> teams, List<Region> regions, int step)
        {
            foreach (var teamID in teams.Keys)
            {
                var streak = 1f;
                while (true)
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

                     if (targets.Count == 0) break; // No target

                    var i = new Random().Next(targets.Count);
                    var target = targets[i];

                    if (// Random invasion fail chance
                        new Random().Next(100) > 60 + team.territory.Count * 3
                        || // Streak fail
                        (streak > 1 && new Random().NextDouble() * streak * (20f / step) > team.territory.Count / 50f))
                    {
                        events.Add(new Repel() { defendedTerritory = target, repelled = team });
                        break; 
                    }

                    // Invasion success
                    events.Add(new Conquest() { conqueredTerritory = target, loser = target.owner, winner = team });
                    var oldOwner = target.owner;
                    team.TakeOwnershipOf(target);
                    if (oldOwner.territory.Count <= 0)
                        events.Add(new Elimination() { eliminated = oldOwner, lastOwnedTerritory = target, killer = team });

                    streak++;
                }
            }
        }
    }
}
