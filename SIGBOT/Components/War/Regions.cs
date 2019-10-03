using System;
using System.Collections.Generic;
using System.Text;

namespace SIGBOT.Components.War
{
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
    }
}
