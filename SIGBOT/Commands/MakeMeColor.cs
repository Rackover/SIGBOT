using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus;
using Newtonsoft.Json;
using SIGBOT.Components;
using System.Linq;

namespace SIGBOT.Commands
{
    public class MakeMeColor : Command
    {
        public readonly string colorsPath = "out/colors.json";


        public async override Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {
            if (!Directory.Exists("out")) Directory.CreateDirectory("out");

            var assoc = new ColoredRoleAssociations();
            if (File.Exists(colorsPath)) {
                while (true) {
                    try {
                        assoc = JsonConvert.DeserializeObject<ColoredRoleAssociations>(File.ReadAllText(colorsPath));
                        break;
                    }
                    catch (IOException e) {
                        Log.Warn("Waiting for file lock READ on " + colorsPath + "\n" + e.ToString());
                        await Task.Delay(100);
                    }
                }
            }

            string colorName = args[0];
            string roleName = args.Length > 1 ? string.Join(" ", args.Skip(1)) : colorName;


            DiscordColor color;

            try {
                color = new DiscordColor(colorName);
            }
            catch (Exception e) {
                await message.RespondAsync("Wrong color given! [" + colorName + "] is not a valid color. Use a #AABBCC format.");
                Log.Warn("When an user tried to set a color:\n" + e.ToString());
                return;
            }
            var guildMember = await message.Channel.Guild.GetMemberAsync(user.Id);

            var fetchedRoles = guildMember.Roles.Where(o => o.Id == assoc[user.Id]?.roleId);
            DiscordRole currentRole;

            if (fetchedRoles.Count() > 0) {
                currentRole = fetchedRoles.ElementAt(0);
                await guildMember.Guild.UpdateRoleAsync(currentRole, roleName, hoist:false, color:color);
            }
            else {
                currentRole = await guildMember.Guild.CreateRoleAsync(roleName, DSharpPlus.Permissions.None, color, hoist:false, mentionable:false);
            }

            await guildMember.GrantRoleAsync(currentRole);

            assoc.ClearFor(user.Id);
            assoc.Add(new ColoredRoleAssociation() { roleId = currentRole.Id, userId = user.Id });

            var me = await message.Channel.Guild.GetMemberAsync(bot.client.CurrentUser.Id);
            var myRole = me.Roles.OrderByDescending(o => o.Position).First();
            var index = myRole.Position - 1;
            await me.Guild.UpdateRolePositionAsync(currentRole, index);
            Log.Trace("Put role " + currentRole.Name + ":"+currentRole.Id+" to position " + index);

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

            /*
            await Task.Run(async delegate {
                guildMember = await message.Channel.Guild.GetMemberAsync(user.Id);
                Log.Info("Roles are now : " + string.Join(",", guildMember.Roles.Select(o => o.Name)));
                Log.Info("Looking for role of which ID is equal to [" + assoc[user.Id]?.roleId + "] in " + guildMember.Roles.Count() + " roles");
                fetchedRoles = guildMember.Roles.Where(o => o.Id == assoc[user.Id]?.roleId);
                Log.Info("Fetched " + fetchedRoles.Count() + " roles...");
                currentRole = fetchedRoles.ElementAt(0);
                await Task.Delay(2000);
                await me.Guild.UpdateRolePositionAsync(currentRole, index);
                Log.Info("Done setting position");
            });
            */
        }
    }
}
