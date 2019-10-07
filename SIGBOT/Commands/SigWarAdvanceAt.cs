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
    class SigWarAdvanceAt : Command
    {
        public override async Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {
            var timeString = args[0];
            bot.chronoEvents.RegisterAtTime(timeString, delegate
            {
                Program.game.ReadFromDisk();
                Program.game.Advance();
                Program.game.WriteToDisk();

                // Message formatting
                var b = new StringBuilder();
                var pair = Program.game.events.Last();
                b.AppendLine("**{0}**".Format(string.Format("{0:F}", pair.Key)));
                foreach (var evnt in pair.Value)
                {
                    b.AppendLine(evnt.ToString());
                }


                message.RespondWithFileAsync(
                    Path.Combine(Program.game.directory, (Program.game.map.step-1) + ".png"),
                    b.ToString()
                );
            });
            await message.RespondAsync("Registered at time "+timeString);
        }
    }
}
