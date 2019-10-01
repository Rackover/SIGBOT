using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace SIGBOT.Commands
{
    class RegisterRoleReact : Command
    {
        public override async Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {
            var emoji = DiscordEmoji.FromUnicode(bot.client, args[0]);
            var roleId = message.Channel.Guild.GetRole(Convert.ToUInt64(args[1]));
            bot.roleOnReact[emoji] = roleId;

            Console.WriteLine("Linked "+emoji+" with "+roleId);
        }
    }
}
