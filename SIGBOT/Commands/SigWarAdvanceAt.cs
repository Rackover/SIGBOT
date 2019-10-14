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
            var finished = false;
            var channel = message.Channel;

            bot.chronoEvents.RegisterAtTime(timeString, async delegate
            {
                if (finished) return;

                var filename = (Program.game.map.step) + ".png";

                Program.game.ReadFromDisk();
                finished = Program.game.Advance();
                Program.game.WriteToDisk();

                // Message formatting

                var b = new StringBuilder();
                var pair = Program.game.events.Last();
                var bar = "--------------------------------------";

                b.AppendLine(bar);
                b.AppendLine("**{0}**".Interpolate(string.Format("{0:F}", pair.Key)));
                b.AppendLine(bar);

                foreach (var evnt in pair.Value)
                {
                    b.AppendLine(evnt.ToString());
                }
                b.AppendLine(bar);
                

                await Program.game.channel.SendFileAsync(
                    Path.Combine(Program.game.directory, filename),
                    b.ToString()
                );
            });
            await message.RespondAsync("Registered at time "+timeString);
        }
    }
}
