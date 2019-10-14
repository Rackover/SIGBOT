using System;
using System.Collections.Generic;
using System.Text;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Components.War.Events
{
    public class Conquest : WarEvent
    {
        public TEAM loser;
        public TEAM winner;
        public int conqueredTerritory;
        public override string ToString()
        {
            var region = Program.game.map.regions[conqueredTerritory];
            var winr = Program.game.map.teams[winner];
            var losr = Program.game.map.teams[loser];
            if (region.history[0] == winner)
                return "**{0}** revient finalement sur le projet **{1}** et abandonne {2}.".Interpolate(region.name, winr.name, losr.name);
            else
                return "**{0}** décide de passer sur le projet **{1}**, abandonnant {2}.".Interpolate(region.name, winr.name, losr.name) ;
        }
    }
}
