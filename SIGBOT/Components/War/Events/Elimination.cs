using System;
using System.Collections.Generic;
using System.Text;

namespace SIGBOT.Components.War.Events
{
    public class Elimination : WarEvent
    {
        public Team eliminated;
        public Team killer;
        public Region lastOwnedTerritory;
        public override string ToString()
        {
            return "**Le projet {0} est annulé car plus personne ne travaille dessus.**".Format(eliminated.name);
        }
    }
}
