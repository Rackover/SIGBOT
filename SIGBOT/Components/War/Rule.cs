﻿using System;
using System.Collections.Generic;
using System.Text;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Components.War
{
    public abstract class Rule
    {
        public abstract void Advance(Teams teams, List<Region> regions, int step);
    }
}
