using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Components.War
{
    [Serializable]
    public class Region
    {
        public int id;
        public string name;
        public List<int> neighbors = new List<int>();
        public byte owner;
        public List<byte> history = new List<byte>();

        // Pivot point of regions is top left (0,0)
        public Vector2 position;
        public Vector2 size = new Vector2(3, 4);

        public Region(string name, Team owner) : this(name, owner, Map.VERTICAL_DESK.X, Map.VERTICAL_DESK.Y){}

        public Region(string name, Team owner, float w, float h)
        {
            this.id = new Random().GetHashCode();
            this.name = name;
            this.owner = owner.id;
            this.history.Add(owner.id);
            owner.territory.Add(this.id);
        }

        [JsonConstructor] public Region() { }

        public Team GetOwner()
        {
            return Program.game.map.teams[owner];
        }

        public void ConnectWith(Region region)
        {
            neighbors.Add(region.id);
            region.neighbors.Add(id);
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
