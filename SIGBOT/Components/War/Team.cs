using SIGBOT.Components.War.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SIGBOT.Components.War
{
    public class Team
    {
        public Color color;
        public string name;
        public Regions territory = new Regions();

        public Team(string name, Color color)
        {
            this.name = name;
            this.color = color;
        }

        public void TakeOwnershipOf(Region region)
        {
            region.owner.territory.RemoveAll(o => o.id == region.id);
            Program.game.events.Add(new Conquest() { conqueredTerritory = region, loser = region.owner, winner = this });
            if (region.owner.territory.Count <= 0)
                Program.game.events.Add(new Elimination() { eliminated = region.owner, lastOwnedTerritory = region, killer = this });

            region.history.Add(region.owner);
            region.owner = this;
            territory[region.id] = region;
        }
    }
}
