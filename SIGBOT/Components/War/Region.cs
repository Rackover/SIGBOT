using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SIGBOT.Components.War
{
    class Region
    {
        public readonly int id;
        public readonly string name;
        public Regions neighbors;
        public Team owner;

        // Pivot point of regions is top left (0,0)
        public Vector2 position;
        public Vector2 size;

        public Region(string name, Team owner, float w=3f, float h=5f)
        {
            this.id = new Random().GetHashCode();
            this.name = name;
            this.owner = owner;
        }

        public void ConnectWith(Region region)
        {
            neighbors[region.id] = region;
            region.neighbors[id] = this;
        }

        public void SetPosition(float x, float y)
        {
            position = new Vector2(x, y);
        }

        public void PutRightOf(Region region)
        {
            SetPosition(region.position.X + region.size.X, region.position.Y);
        }

        public void PutLeftOf(Region region)
        {
            SetPosition(region.position.X - size.X, region.position.Y);
        }

        public void PutBelow(Region region)
        {
            SetPosition(region.position.X, region.position.Y + region.size.Y);
        }

        public void PutAbove(Region region)
        {
            SetPosition(region.position.X, region.position.Y - size.Y);
        }
    }
}
