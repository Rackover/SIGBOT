using System;
using System.Collections.Generic;
using System.Text;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Components.War.Events
{
    public class Supremacy : WarEvent
    {
        public TEAM winner;
        public int lastConquest;

        public override string ToString()
        {
            var winr = Program.game.map.teams[winner];
            return "**Tout le monde travaille désormais sur le projet {0}.**".Interpolate(winr.name);
        }
    }
}
