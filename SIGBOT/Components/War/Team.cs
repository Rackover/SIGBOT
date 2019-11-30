using SIGBOT.Components.War.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Components.War
{
    [Serializable]
    public class Team
    {
        public Color color;
        public string name;
        public List<int> territory = new List<int>();
        public TEAM id;
        public string font;

        public Team(string name, TEAM id, Color color, string font=null)
        {
            this.name = name;
            this.color = color;
            this.id = id;
            this.font = font;
        }

        public void TakeOwnershipOf(Region region)
        {
            region.GetOwner().territory.RemoveAll(o => o == region.id); 
            Program.game.events.Add(new Conquest() { conqueredTerritory = region.id, loser = region.owner, winner = id });
            if (region.GetOwner().territory.Count <= 0)
                Program.game.events.Add(new Elimination() { eliminated = region.owner, lastOwnedTerritory = region.id, killer = id });

            region.history.Add(region.owner);
            region.owner = id;
            territory.Add(region.id);
        }

        public List<Region> GetTerritory()
        {
            return Program.game.map.regions.FindAll(
                o => o.owner == id
            );
        }
    }
}
