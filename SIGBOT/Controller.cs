using System;
using System.Collections.Generic;
using System.Text;

namespace SIGBOT
{
    public class Controller : Dictionary<string, Command>
    {
        public Controller(){
            Add("ping", new Commands.Ping());
            Add("listenreacts", new Commands.ListenRoles());
            Add("register", new Commands.RegisterRoleReact());
            Add("sigwarstart", new Commands.SigWarStart());
            Add("sigwarload", new Commands.SigWarLoad());
            Add("sigwaradvanceat", new Commands.SigWarAdvanceAt());
            Add("clearchrono", new Commands.ChronoClear());
            Add("clock", new Commands.Clock());
            Add("sigwarstatus", new Commands.SigWarStatus());
            Add("sigwarremovetick", new Commands.SigWarRemoveTick());
            Add("sigwarhere", new Commands.SigWarHere());
            Add("sigwarskipday", new Commands.SigWarSkipDay());
        }
    }
}
