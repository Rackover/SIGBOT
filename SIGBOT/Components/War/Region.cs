using System;
using System.Collections.Generic;
using System.Text;

namespace SIGBOT.Components.War
{
    class Region
    {
        public readonly int id;
        public Regions neighbors;
        public Team owner;

        public Region(int id, Team owner)
        {
            this.id = id;
            this.owner = owner;
        }

        public void ConnectWith(Region region)
        {
            neighbors[region.id] = region;
            region.neighbors[id] = this;
        }
    }
}
