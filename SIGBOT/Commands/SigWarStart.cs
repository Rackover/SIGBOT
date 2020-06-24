using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using SIGBOT.Components.War;
using SIGBOT.Components.War.Rules;

namespace SIGBOT.Commands
{
    class SigWarStart : Command
    {
        public override async Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {
            var ruleName = args[0];
            Log.Info("Starting SIGWAR with rule "+ruleName+"...");

            // Rule used
            try {
                var rule = Game.rules.Where(o => o.Key.ToLower() == ruleName.ToLower()).First().Value;

                new Game(rule);

                Program.game.channel = message.Channel;

                await new SigWarStatus().Execute(bot, user, message, args);

                await message.RespondAsync("Tout va bien dans la salle.");
                Log.Info("SIGWAR started. Register ticks to get it going with >SigWarAdvanceAt HH:MM:SS");
            }
            catch (InvalidOperationException) {
                await message.RespondAsync("Rule does not exist: " + ruleName+". Rules are:\n"+string.Join("\n", Game.rules.Keys.Select(o=>o.ToString())));
                Log.Warn("Rule does not exist: "+ruleName);
            }
        }
    }
}
