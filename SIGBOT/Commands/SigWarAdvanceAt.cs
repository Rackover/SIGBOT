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

            if (bot.chronoEvents.events.Count <= 0)
            {
                Program.game.ReadFromDisk();
                var disp = Program.game.display;
                disp.WriteToDisk(
                    disp.DrawMap(
                        Program.game.map.regions,
                        Program.game.map.teams.ToList()
                    ),
                    Path.Combine(Program.game.directory, Program.game.map.step + ".png")
                );
                await channel.SendFileAsync(
                    Path.Combine(Program.game.directory, (Program.game.map.step) + ".png"),
                    "Tout est calme dans la salle."
                );
            }

            bot.chronoEvents.RegisterAtTime(timeString, async delegate
            {
                if (finished) return;
                Program.game.ReadFromDisk();
                finished = Program.game.Advance();
                Program.game.WriteToDisk();

                // Message formatting
                var b = new StringBuilder();
                var pair = Program.game.events.Last();
                b.AppendLine("**{0}**".Format(string.Format("{0:F}", pair.Key)));
                foreach (var evnt in pair.Value)
                {
                    b.AppendLine(evnt.ToString());
                }


                Console.WriteLine(">>Sending message with file<<");
                await channel.SendFileAsync(
                    Path.Combine(Program.game.directory, (Program.game.map.step) + ".png"),
                    b.ToString()
                );
            });
            await message.RespondAsync("Registered at time "+timeString);
        }
    }
}
