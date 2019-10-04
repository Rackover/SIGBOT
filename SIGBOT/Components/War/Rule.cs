using System;
using System.Collections.Generic;
using System.Text;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Components.War
{
    public abstract class Rule
    {
        public Journal events = new Journal();
        public abstract void Advance(Dictionary<TEAM, Team> teams, List<Region> regions, int step);
    }
}
