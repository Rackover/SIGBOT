using System;
using System.Collections.Generic;
using System.Text;

namespace SIGBOT.Components.War
{
    public class Journal : Dictionary<DateTime, List<WarEvent>>
    {
        public void Add(WarEvent evnt)
        {
            if (!ContainsKey(Game.now)) Add(Game.now, new List<WarEvent>());
            this[Game.now].Add(evnt);
        }

        public override string ToString()
        {
            var b = new StringBuilder();

            foreach(var pair in this)
            {
                b.AppendLine("**{0}**".Format(string.Format("{0:F}", pair.Key))); ;
                foreach(var evnt in pair.Value)
                {
                    b.AppendLine(evnt.ToString());
                }
                b.AppendLine();
            }

            return b.ToString();
        }
    }
}
