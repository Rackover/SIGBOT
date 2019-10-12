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
    class SigWarStatus : Command
    {
        public override async Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {
            Program.game.ReadFromDisk();
            var disp = Program.game.display;
            disp.WriteToDisk(
                disp.DrawMap(
                    Program.game.map.regions,
                    Program.game.map.teams.ToList()
                ),
                Path.Combine(Program.game.directory, "status.png")
            );

            var b = new StringBuilder();
            var pair = Program.game.events.Last();
            var bar = "--------------------------------------";

            b.AppendLine(bar);
            b.AppendLine("**{0}**".Format(string.Format("{0:F}", pair.Key)));
            b.AppendLine(bar);

            foreach (var evnt in pair.Value) {
                b.AppendLine(evnt.ToString());
            }
            b.AppendLine(bar);

            await message.Channel.SendFileAsync(
                Path.Combine(Program.game.directory, "status.png"),
                b.ToString()
            );

        }
    }
}
