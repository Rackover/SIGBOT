using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGBOT.Components
{
    public class RoleOnReact : Dictionary<DiscordEmoji, DiscordRole>
    {
        public DiscordMessage message;
    }
}
