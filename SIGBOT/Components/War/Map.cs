using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SIGBOT.Components.War
{
    class Map
    {
        public Regions regions;
        public Dictionary<string, Team> teams;

        public Map()
        {
            // Initializing every team
            teams.Add("planetFog", new Team("A planet in the fog", Color.WhiteSmoke));
            teams.Add("plipPlop", new Team("Plip plop", Color.Pink));
            teams.Add("ripple", new Team("Ripple", Color.Lime));
            teams.Add("battleCars", new Team("Battle cars", Color.Yellow));
            teams.Add("ombre", new Team("Ombre", Color.Pink));
            teams.Add("cookie", new Team("Tough Kookie", Color.Pink));
            teams.Add("haulAway", new Team("Haul away", Color.Pink));
        }
    }
}
