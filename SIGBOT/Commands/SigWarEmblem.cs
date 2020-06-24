﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using DSharpPlus.Entities;
using NeoSmart.Unicode;

namespace SIGBOT.Commands
{
    class SigWarEmblem : Command
    {
        public override async Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {
            if (args.Length < 1) {
                await message.RespondAsync("Please supply your emblem as an argument.\nIt should be exactly one emoji, nothing before or after.");
                return;
            }
            
            string emojiUTF = args[0];

            DiscordEmoji guildEmoji = Extensions.ToDiscordEmoji(emojiUTF, bot);

            if (guildEmoji == null) {
                await message.RespondAsync("Unknown emoji: [" + emojiUTF + "].\nEmblem could not be set.");
                return;
            }

            if (Program.game == null || Program.game.map == null) {
                await message.RespondAsync("Sigwar does not seem to be running.\nEmblem could not be set.");
                return;
            }

            Program.game.map.emblems.SetEmblem(user.Id, guildEmoji.ToString());

            await message.RespondAsync("Emblem set: "+guildEmoji.ToString()+".\nPlease wait for the next curse or message update for it to refresh.");
        }
    }
}
