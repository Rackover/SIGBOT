using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using SIGBOT.Components.War.Rules;

namespace SIGBOT.Commands
{
    class SigWarCurse : Command
    {
        public override async Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {
            if (Program.game.rule is OneTakeRandomStreakCurse)
            {

            }
            else
            {
                await message.RespondAsync("The game is not running, or the ruleset does not support a Curse command.");
            }
        }
    }
}
