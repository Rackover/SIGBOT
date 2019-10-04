using System;
using System.Collections.Generic;
using System.Text;

namespace SIGBOT.Components.War.Events
{
    public class Supremacy : WarEvent
    {
        public Team winner;
        public Region lastConquest;

        public override string ToString()
        {
            return "**Tout le monde travaille désormais sur le projet {0}.**".Format(winner.name);
        }
    }
}
