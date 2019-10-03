using SIGBOT.Components.War;
using System;

namespace SIGBOT
{
    class Program
    {
        static void Main(string[] args)
        {
            new Game(new Components.War.Rules.MultipleTryRandomStreak()).Run();
            //new Bot(Environment.GetEnvironmentVariable("TOKEN"));
        }
    }
}
