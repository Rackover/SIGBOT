using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using SIGBOT.Components.War.Rules;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Commands
{
    class SigWarCurse : Command
    {
        public List<DiscordUser> curseGivers = new List<DiscordUser>();

        public override async Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {
            if (Program.game.rule is OneTakeRandomStreakCurse)
            {
                var curseRule = (OneTakeRandomStreakCurse)Program.game.rule;
                if (curseRule.canResetCurseGivers)
                {
                    curseGivers.Clear();
                    curseRule.canResetCurseGivers = false;
                }

                string givenName = args[0].Replace(" ", string.Empty);
                TEAM target;

                // Let's see if the supplied team name is an enum (like PLIPPLOP, PLANETFOG, etc...)
                var validTeam = Enum.TryParse<TEAM>(givenName, true, out target);

                if (!validTeam)
                {
                    // Not a valid enum ? Maybe it's a team name...
                    var team = Program.game.map.teams.Find(o => o.name.Replace(" ", string.Empty).Equals(givenName, StringComparison.OrdinalIgnoreCase));
                    if (team == null)
                    {
                        await message.RespondAsync("I couldn't find the team [" + args[0] + "].\nPlease fire >sigwarteams to get a list of keywords.\nNo curse was cast.");
                        return;
                    }
                    target = team.id;
                }

                if (!curseRule.curses.ContainsKey(target))
                    curseRule.curses[target] = 0;

                if (curseGivers.Contains(user))
                {
                    await message.RespondAsync("You have already cast a curse for the next battle.\nYou can no longer cast a curse at the moment.\nNo curse was cast.");
                    return;
                }

                curseRule.curses[target] += 1; //Added one curse
                await message.RespondAsync("The curse on `"+target+"`  is cast!");

            }
            else
            {
                await message.RespondAsync("The game is not running, or the current ruleset does not support a Curse command.");
            }
        }
    }
}
