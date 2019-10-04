using System;
using System.Collections.Generic;
using System.Text;

namespace SIGBOT.Components.War.Events
{
    public class Conquest : WarEvent
    {
        public Team loser;
        public Team winner;
        public Region conqueredTerritory;
        public override string ToString()
        {
            if (conqueredTerritory.history[0] == winner)
                return "{0} revient finalement sur le projet {1} et abandonne {2}.".Format(conqueredTerritory.name, winner.name, loser.name);
            else
                return "{0} décide de passer sur le projet {1}, abandonnant {2}.".Format(conqueredTerritory.name, winner.name, loser.name) ;
        }
    }
}
