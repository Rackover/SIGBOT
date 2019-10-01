using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;

namespace SIGBOT
{
    public abstract class Command
    {
        public abstract Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args); 
    }
}
