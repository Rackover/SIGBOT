using DSharpPlus.Entities;
using SIGBOT.Components.War;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGBOT.Commands
{
    class SigWarRemoveTick : Command
    {
        public override async Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {
            var timeString = args[0];
            var removed = bot.chronoEvents.ClearAtTime(timeString);
            await message.RespondAsync(removed ? "Cleared time " +timeString : "Nothing to deregister");
        }
    }
}
