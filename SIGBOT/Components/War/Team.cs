using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SIGBOT.Components.War
{
    class Team
    {
        public Color color;
        public string name;
        public Regions territory;

        public Team(string name, Color color)
        {
            this.name = name;
            this.color = color;
        }

        public void Cede(Region region, Team originalOwner)
        {
            region.owner = this;
            originalOwner.territory.RemoveAll(o => o.id == region.id);
            territory[region.id] = region;
        }
    }
}
