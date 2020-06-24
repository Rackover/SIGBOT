using System;
using System.Collections.Generic;
using System.Text;

namespace SIGBOT.Components
{
    public class ColoredRoleAssociations : List<ColoredRoleAssociation>
    {

        public ColoredRoleAssociation this[ulong userId]{
            get {
                return Find(o => o.userId == userId);
            }
        }

        public void ClearFor(ulong userId)
        {
            RemoveAll(o => o.userId == userId);
        }
    }
}
