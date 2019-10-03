using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Numerics;

namespace SIGBOT.Components.War
{
    public class Map
    {
        static public readonly Vector2 HORIZONTAL_DESK = new Vector2(4, 3);
        static public readonly Vector2 LARGE_HORIZONTAL_DESK = new Vector2(5, 3);
        static public readonly Vector2 VERTICAL_DESK = new Vector2(3, 4);
        static public readonly Vector2 LONG_VERTICAL_DESK = new Vector2(3, 5);

        public enum TEAM { 
            PLIPPLOP, 
            PLANETFOG, 
            RIPPLE, 
            BATTLECARS, 
            OMBRES, 
            TOUGHCOOKIE, 
            HAULAWAY, 
            ROBOTS, 
            NEUTRAL }
        public Regions regions = new Regions();
        public Dictionary<TEAM, Team> teams = new Dictionary<TEAM, Team>();
    }

}
