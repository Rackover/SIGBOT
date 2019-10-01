using System;
using System.Collections.Generic;
using System.Text;

namespace SIGBOT
{
    public class Controller : Dictionary<string, Command>
    {
        public Controller(){
            Add("ping", new Commands.Ping());
        }
    }
}
