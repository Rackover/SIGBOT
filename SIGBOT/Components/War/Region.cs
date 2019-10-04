using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SIGBOT.Components.War
{
    public class Region
    {
        public readonly int id;
        public readonly string name;
        public Regions neighbors = new Regions();
        public Team owner;
        public List<Team> history = new List<Team>();

        // Pivot point of regions is top left (0,0)
        public Vector2 position;
        public Vector2 size = new Vector2(3, 4);

        public Region(string name, Team owner) : this(name, owner, Map.VERTICAL_DESK.X, Map.VERTICAL_DESK.Y){}

        public Region(string name, Team owner, float w, float h)
        {
            this.id = new Random().GetHashCode();
            this.name = name;
            this.owner = owner;
            this.history.Add(owner);
            this.owner.territory[this.id] = this;
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

        public void SetSize(float x, float y)
        {
            size = new Vector2(x, y);
        }

        public void SetSize(Vector2 size)
        {
            this.size = new Vector2(size.X, size.Y);
        }

        public void PutEastOf(Region region)
        {
            SetPosition(region.position.X + region.size.X - 1, region.position.Y);
        }

        public void PutWestOf(Region region)
        {
            SetPosition(region.position.X - size.X + 1, region.position.Y);
        }

        public void PutSouthOf(Region region)
        {
            SetPosition(region.position.X, region.position.Y + region.size.Y - 1);
        }

        public void PutNorthOf(Region region)
        {
            SetPosition(region.position.X, region.position.Y - size.Y + 1);
        }
    }
}
