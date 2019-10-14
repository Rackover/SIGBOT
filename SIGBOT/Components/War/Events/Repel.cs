using System;
using System.Collections.Generic;
using System.Text;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Components.War.Events
{
    public class Repel : WarEvent
    {
        public TEAM repelled;
        public int defendedTerritory;
        public override string ToString()
        {
            var target = Program.game.map.regions[defendedTerritory];
            var repelld = Program.game.map.teams[repelled];
            return "{0} songe a travailler sur le projet {1}, mais garde le cap.".Interpolate(target.name, repelld.name);
        }
    }
}
