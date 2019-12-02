using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using SIGBOT.Components.War;
using SIGBOT.Components.War.Rules;
using static SIGBOT.Extensions;

namespace SIGBOT.Commands
{
    class SigWarStatus : Command
    {
        public override async Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {
            if (Program.game == null)
            {
                await message.RespondAsync("Sigwar isn't running, no status can be read.");
                return;
            }

            Log.Trace("Requested status, reading from disk...");
            Program.game.ReadFromDisk();
            if (Program.game.map == null)
            {
                await message.RespondAsync("Couldn't read the status from disk (?)");
                return;
            }
            var disp = Program.game.display;
            disp.WriteToDisk(
                disp.DrawMap(
                    Program.game.map.regions,
                    Program.game.map.teams.ToList()
                ),
                Path.Combine(Program.game.directory, "status.png")
            );
            Log.Trace("Created status, building string.");

            var b = new StringBuilder();

            var bar = "--------------------------------------";

            if (Program.game.events.Count > 0)
            {
                var pair = Program.game.events.Last();
                b.AppendLine(bar);
                b.AppendLine("**{0}**".Interpolate(string.Format("{0:F}", pair.Key)));
                b.AppendLine(bar);

                foreach (var evnt in pair.Value)
                {
                    b.AppendLine(evnt.ToString());
                }
                b.AppendLine(bar);
            }

            Log.Trace("Ready to send status message...");

            await message.Channel.SendFileAsync(
                Path.Combine(Program.game.directory, "status.png"),
                b.ToString()
            );
            Log.Trace("Sent");

        }
    }
}
