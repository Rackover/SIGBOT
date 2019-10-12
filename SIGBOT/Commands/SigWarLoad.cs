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
            Console.WriteLine("Loading SIGWAR...");

            // Rule used
            var rule = new OneTakeRandomStreak();

            new Game(rule, true);

            Program.game.channel = message.Channel;

            await new SigWarStatus().Execute(bot, user, message, args);

            Console.WriteLine("SIGWAR loaded. Register ticks to get it going with >SigWarAdvanceAt HH:MM:SS");
        }
    }
}
