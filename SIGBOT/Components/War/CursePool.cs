using System;
using System.Collections.Generic;
using System.Text;

namespace SIGBOT.Components.War
{
    [Serializable]
    public class CursePool
    {
        public byte teamId;
        public List<ulong> cursers = new List<ulong>();
    }
}
