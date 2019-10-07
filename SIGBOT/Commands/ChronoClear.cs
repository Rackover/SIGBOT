using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace SIGBOT.Commands
{
    class ChronoClear : Command
    {
        public override async Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {
            bot.chronoEvents.Clear();
            await message.RespondAsync("SIGWAR cleared");
        }
    }
}
