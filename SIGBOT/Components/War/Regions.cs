using System;
using System.Collections.Generic;
using System.Text;

namespace SIGBOT.Components.War
{
    [Serializable]
    public class Regions : List<Region>
    {
        new public Region this[int i]{
            get { return Find(o => o.id == i); }
            set {
                RemoveAll(o => o.id == i);
                Add(value);
            }
        }

        public Region this[string name] {
            get { return Find(o => o.name == name); }
        }

        public void Rearrange()
        {
            var newRegions = new Regions();
            foreach(var region in this)
            {
                newRegions[region.id] = region;
            }

            Clear();
            foreach (var region in newRegions)
            {
                this[region.id] = region;
            }
        }
    }
}
