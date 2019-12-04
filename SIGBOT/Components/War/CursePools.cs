using System;
using System.Collections.Generic;
using System.Text;

namespace SIGBOT.Components.War
{
    [Serializable]
    public class CursePools : List<CursePool>
    {
        public void AddCurse(byte target, ulong instigator)
        {
            var pool = Find(o => o.teamId == target);
            if (pool == null) {
                pool = new CursePool() { teamId = target };
                Add(pool);
            }
            pool.cursers.RemoveAll(o=>o==instigator);
            pool.cursers.Add(instigator);
            Program.game.WriteToDisk();
        }

        public bool HasCastCurse(ulong curser)
        {
            return Find(o => o.cursers.Contains(curser)) != null;
        }
    }
}
