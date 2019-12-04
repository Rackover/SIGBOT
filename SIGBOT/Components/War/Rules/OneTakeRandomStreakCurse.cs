using SIGBOT.Components.War.Events;
using System;
using System.Collections.Generic;
using System.Text;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Components.War.Rules
{
    public class OneTakeRandomStreakCurse : Rule, ICurseable
    {
        readonly int CURSE_POWER = 5;

        public bool canResetCurseGivers = true;

        Dictionary<byte, int> curses = new Dictionary<byte, int>();

        public void AddCurse(byte target)
        {
            if (!curses.ContainsKey(target)) curses.Add(target, 0);
            curses[target]+= CURSE_POWER;
        }

        public void AddCurses(byte target, int amount)
        {
            for (int i = 0; i < amount; i++) {
                AddCurse(target);
            }
        }

        public void SetCurses(byte target, int amount)
        {
            if (!curses.ContainsKey(target)) curses.Add(target, 0);
            curses[target] = amount;
        }

        public bool ShouldResetCurses()
        {
            return canResetCurseGivers;
        }

        public void DeclareCursesResetted()
        {
            canResetCurseGivers = false;
        }

        public override void Advance(Teams teams, List<Region> regions, int step)
        {
            var teamPickList = new List<byte>();

            foreach (var team in teams) {

                if (!curses.ContainsKey(team.id)) curses.Add(team.id, 0);

                Log.Trace("STAT: Team " + team.name + " has " + curses[team.id] + " power of curse on their back.");
                foreach(var _ in team.territory)
                {
                    foreach (var notMe in teams.FindAll(o => o.id != team.id)) {
                        if (!curses.ContainsKey(notMe.id)) curses.Add(notMe.id, 0);
                        for (int i = 0; i <= curses[notMe.id]; i++) {
                            // For each curse that is not on me, I add myself to the list
                            teamPickList.Add(team.id);
                        }
                    }
                }
            }

            // Statistics
            var stats = new Dictionary<byte, int>();
            int total = 0;
            foreach(var potentialTarget in teamPickList)
            {
                if (!stats.ContainsKey(potentialTarget)) stats[potentialTarget] = 0;
                stats[potentialTarget]++;
                total++;
            }
            foreach(var team in teams)
            {
                Log.Trace("STAT: Team " + team.name + " has " + MathF.Round((stats[team.id] / (float)total) * 100f) + "% chances to play!");
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
                    foreach (var neighbor in region.neighbors.FindAll(o => Program.game.map.regions[o].owner != team.id)) {
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
