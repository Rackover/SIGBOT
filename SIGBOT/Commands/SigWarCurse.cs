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
            if (args.Length < 1)
            {
                await message.RespondAsync("You need to supply a target for the curse.\nNo curse was cast.");
                return;
            }

            if (Program.game != null && Program.game.rule is OneTakeRandomStreakCurse)
            {
                var curseRule = (OneTakeRandomStreakCurse)Program.game.rule;
                if (curseRule.canResetCurseGivers)
                {
                    curseGivers.Clear();
                    curseRule.canResetCurseGivers = false;
                }

                string givenName = string.Join(' ', args).Replace(" ", string.Empty);
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
                curseGivers.Add(user);
                await message.RespondAsync("The curse on `"+target+"` is cast!\nMay their fate be terrible and their life short.");

                var logo = user.Id % 5 == 0 ? "☄" : user.Id % 3 == 0 ? "⚡" : user.Id % 2 == 0 ? "🌟" : "🌠";
                await Program.game.channel.SendMessageAsync("A curse was cast upon " + Program.game.map.teams.Find(o => o.id == target).name + "! "+logo);

            }
            else
            {
                await message.RespondAsync("The game is not running, or the current ruleset does not support a Curse command.");
            }
        }
    }
}
