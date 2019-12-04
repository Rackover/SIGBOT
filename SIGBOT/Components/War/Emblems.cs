using System;
using System.Collections.Generic;
using System.Text;

namespace SIGBOT.Components.War
{
    [Serializable]
    public class Emblems : List<Emblem>
    {
        public void SetEmblem(ulong discordUserId, string emblem)
        {
            RemoveAll(o => o.discordUserId == discordUserId);
            Add(new Emblem() { discordUserId = discordUserId, character = emblem });
            Program.game.WriteToDisk();
        }

        public bool HasEmblem(ulong discordUserId)
        {
            return GetEmblem(discordUserId) != null;
        }

        public Emblem GetEmblem(ulong discordUserId)
        {
            return Find(o => o.discordUserId == discordUserId);
        }
    }
}
