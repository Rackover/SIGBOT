using System;
using System.Collections.Generic;
using System.Linq;

namespace SIGBOT
{
    class Program
    {
        static void Main(string[] args)
        {
            var display = new SIGBOT.Components.War.Display(scale:10);
            var map = new SIGBOT.Components.War.Classroom.J002();
            display.WriteToDisk(
                display.DrawMap(
                    map.regions,
                    map.teams.Values.ToList()
                )
            );
            //new Bot(Environment.GetEnvironmentVariable("TOKEN"));
        }
    }
}
