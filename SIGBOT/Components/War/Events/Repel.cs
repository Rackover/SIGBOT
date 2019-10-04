using System;
using System.Collections.Generic;
using System.Text;

namespace SIGBOT.Components.War.Events
{
    public class Repel : WarEvent
    {
        public Team repelled;
        public Region defendedTerritory;
        public override string ToString()
        {
            return "{0} songe a travailler sur le projet {1}, mais garde le cap.".Format(defendedTerritory.name, repelled.name);
        }
    }
}
