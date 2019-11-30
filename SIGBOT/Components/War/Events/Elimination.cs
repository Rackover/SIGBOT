using System;
using System.Collections.Generic;
using System.Text;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Components.War.Events
{
    public class Elimination : WarEvent
    {
        public byte eliminated;
        public byte killer;
        public int lastOwnedTerritory;
        public override string ToString()
        {
            var elim = Program.game.map.teams[eliminated];
            return "**Le projet {0} est annulé car plus personne ne travaille dessus.**".Interpolate(elim.name);
        }
    }
}
