using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using SIGBOT.Components.War.Rules;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Commands
{
    class SigWarSetCurses : Command
    {
		public override async Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {
            if (args.Length < 2)
            {
                Log.Trace("Wrong number of arguments for this command");
                await message.RespondAsync("You need to supply a target and a number for the curses.");
                return;
            }

            if (Program.game != null && Program.game.rule is ICurseable) {
                Log.Trace("Casting rule to a curse rule");
                var curseRule = (ICurseable)Program.game.rule;

                string givenName = string.Join(' ', args.Skip(1)).Replace(" ", string.Empty);
                Log.Trace("Given name is "+givenName);
                int amount = Convert.ToInt32(args[0]);
                Log.Trace("Amount is " + amount);


                Log.Trace("Looking for team "+givenName);
                var team = Program.game.map.teams.Find(o => o.name.Replace(" ", string.Empty).Equals(givenName, StringComparison.OrdinalIgnoreCase));
                if (team == null)
                {
                    await message.RespondAsync("I couldn't find the team [" + args[0] + "].\nPlease fire >sigwarteams to get a list of keywords.\nNo curse was cast.");
                    return;
                }
                byte target = team.id;

                Log.Trace("Setting "+amount+" curses to "+team.id);

                curseRule.SetCurses(target, amount);

                await message.RespondAsync("Curses for [" + target + "] set to "+amount+".");
            }
            else
            {
                await message.RespondAsync("The game is not running, or the current ruleset does not support a Curse command.");
            }
        }
    }
}
