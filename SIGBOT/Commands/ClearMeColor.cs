using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using Newtonsoft.Json;
using SIGBOT.Components;

namespace SIGBOT.Commands
{
    public class ClearMeColor : Command
    {
        public readonly string colorsPath = "out/colors.json";

        public override async Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {
            if (!Directory.Exists("out")) Directory.CreateDirectory("out");

            var assoc = new ColoredRoleAssociations();
            if (File.Exists(colorsPath)) {
                while (true) {
                    try {
                        assoc = (ColoredRoleAssociations)JsonConvert.DeserializeObject<ColoredRoleAssociations>(File.ReadAllText(colorsPath));
                        break;
                    }
                    catch (IOException e) {
                        // Retry
                        Log.Warn("Waiting for file lock READ on " + colorsPath+"\n"+e.ToString());
                        await Task.Delay(100);
                    }
                }
            }

            var guildMember = await message.Channel.Guild.GetMemberAsync(user.Id);
            var fetchedRoles = guildMember.Roles.Where(o => o.Id == assoc[user.Id]?.roleId);
            DiscordRole currentRole;

            if (fetchedRoles.Count() > 0) {
                currentRole = fetchedRoles.ElementAt(0);

                await guildMember.RevokeRoleAsync(currentRole);
                await guildMember.Guild.DeleteRoleAsync(currentRole);
            }

            assoc.ClearFor(user.Id);

            while (true) {
                try {
                    File.WriteAllText(colorsPath, JsonConvert.SerializeObject(assoc));
                    break;
                }
                catch (IOException e) {
                    Log.Warn("Waiting for file lock WRITE on " + colorsPath + "\n" + e.ToString());
                    await Task.Delay(100);
                }
            }

            await message.CreateReactionAsync(Extensions.ToDiscordEmoji("✅", bot));
        }
    }
}
