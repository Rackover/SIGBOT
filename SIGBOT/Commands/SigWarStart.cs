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
            Console.WriteLine("Starting SIGWAR...");

            // Rule used
            var rule = new OneTakeRandomStreakCurse();

            new Game(rule);

            Program.game.channel = message.Channel;

            await new SigWarStatus().Execute(bot, user, message, args);

            await message.RespondAsync("Tout va bien dans la salle.");
            Console.WriteLine("SIGWAR started. Register ticks to get it going with >SigWarAdvanceAt HH:MM:SS");
        }
    }
}
