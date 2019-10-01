using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace SIGBOT.Commands
{
    class ListenRoles : Command
    {
        public override async Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {
            bot.roleOnReact.message = await message.Channel.GetMessageAsync(Convert.ToUInt64(args[0]));
            
            Console.WriteLine("Replaced role listening message id with " + message.Id);
        }
    }
}
