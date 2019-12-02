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
    class SigWarLoad : Command
    {
        public override async Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {
            Log.Info("Loading SIGWAR...");

            // Rule used
            var rule = new OneTakeRandomStreakCurse();
            Log.Trace("Loaded rule...");

            new Game(rule, true);
            Log.Trace("Created game!");

            Program.game.channel = message.Channel;

            Log.Trace("Loaded, triggering status...");

            await new SigWarStatus().Execute(bot, user, message, args);

            Log.Info("SIGWAR loaded. Register ticks to get it going with >SigWarAdvanceAt HH:MM:SS");
        }
    }
}
