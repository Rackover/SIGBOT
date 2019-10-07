using System;
using System.Collections.Generic;
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
            await message.RespondAsync("Starting SIGWAR...");
            new Game(new OneTakeRandomStreak());
            await message.RespondAsync("SIGWAR started. Register ticks to get it going with >SigWarAdvanceAt HH:MM:SS");
        }
    }
}
