using System;
using System.Collections.Generic;
using System.Text;

namespace SIGBOT.Components.War.Rules
{
    interface ICurseable
    {
        void AddCurses(byte target, int amount);
        void SetCurses(byte target, int amount);
        void AddCurse(byte target);
        bool ShouldResetCurses();
        void DeclareCursesResetted();
    }
}
