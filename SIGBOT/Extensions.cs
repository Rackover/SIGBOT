using DSharpPlus.Entities;
using NeoSmart.Unicode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGBOT
{
    public static class Extensions
    {
        public static string Interpolate(this string str, params string[] args)
        {
            return string.Format(str, args);
        }

        public static DiscordEmoji ToDiscordEmoji(string emojiUTF, Bot bot)
        {
            DiscordEmoji guildEmoji;
            try {
                guildEmoji = DiscordEmoji.FromName(bot.client, emojiUTF);
            }
            catch {
                try {
                    guildEmoji = DiscordEmoji.FromName(bot.client, string.Format(":{0}:", emojiUTF.Split(':')[1]));
                }
                catch {
                    try {
                        if (Emoji.All.ToList().FindAll(o => o.Sequence.AsString == emojiUTF).Count <= 0) throw;
                        guildEmoji = DiscordEmoji.FromUnicode(bot.client, emojiUTF);
                    }
                    catch {
                        guildEmoji = null;
                    }
                }
            }

            return guildEmoji;
        }
    }
}
