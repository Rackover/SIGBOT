using System;
using System.Collections.Generic;
using System.Text;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Components.War
{
    [Serializable]
    public class Teams : List<Team>
    {
        public Team this[TEAM id] {
            get {
                return Find(o => o.id == id); 
            }
            set
            {
                RemoveAll(o => o.id == value.id);
                Add(value);
            }
        }

        public void Rearrange()
        {
            var newTeams = new Teams();
            foreach (var team in this)
            {
                newTeams[team.id] = team;
            }

            Clear();
            foreach (var team in newTeams)
            {
                this[team.id] = team;
            }
        }
    }
}
