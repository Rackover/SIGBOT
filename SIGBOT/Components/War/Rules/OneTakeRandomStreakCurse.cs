using SIGBOT.Components.War.Events;
using System;
using System.Collections.Generic;
using System.Text;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Components.War.Rules
{
    public class OneTakeRandomStreakCurse : Rule
    {
        public Dictionary<TEAM, int> curses = new Dictionary<TEAM, int>();
        public bool canResetCurseGivers = true;

        public override void Advance(Teams teams, List<Region> regions, int step)
        {
            var teamPickList = new List<TEAM>();

            foreach (var team in teams)
            {
                foreach(var _ in team.territory)
                {
                    foreach (var notMe in teams.FindAll(o => o.id != team.id))
                    {
                        if (!curses.ContainsKey(notMe.id)) curses[notMe.id] = 0;
                        for (int i = 0; i <= curses[notMe.id]; i++)
                        {
                            // For each curse that is not on me, I add myself to the list
                            teamPickList.Add(team.id);
                        }
                    }
                }
            }

            // Statistics
            var stats = new Dictionary<TEAM, int>();
            int total = 0;
            foreach(var potentialTarget in teamPickList)
            {
                if (!stats.ContainsKey(potentialTarget)) stats[potentialTarget] = 0;
                stats[potentialTarget]++;
                total++;
            }
            foreach(var team in teams)
            {
                Console.WriteLine("STAT: Team " + team.id + " has " + MathF.Round((stats[team.id] / (float)total) * 100f) + "% chances to play!");
            }
            // Endof


            var attacker = teamPickList[new Random().Next(teamPickList.Count)];
            var streak = 1f;
            while (true)
            {
                var team = teams[attacker];
                var targets = new List<Region>();
                foreach (var region in team.GetTerritory())
                {
                    foreach (var neighbor in region.neighbors.FindAll(o => Program.game.map.regions[o].owner != team.id))
                    {
                        for (var j = 0; j <= curses[team.id]; j++) {
                            targets.Add(Program.game.map.regions[neighbor]);
                        }
                    }
                }

                if (targets.Count == 0) break; // No possible target

                var i = new Random().Next(targets.Count);
                var target = targets[i];

                if (// Streak fail
                    (streak > 1 && new Random().NextDouble() * streak * (20f / step) > ((team.territory.Count-curses[team.id]) / 50f)))
                {
                    Program.game.events.Add(new Repel() { defendedTerritory = target.id, repelled = team.id });
                    break; 
                }

                // Invasion success
                team.TakeOwnershipOf(target);

                streak++;
            }
            
            canResetCurseGivers = true;
            curses.Clear();
        }
    }
}
