using SIGBOT.Components.War;
using System;

namespace SIGBOT
{
    class Program
    {
        public static Game game;
        static void Main(string[] args)
        {
            game = new Game(new Components.War.Rules.MultipleTryRandomStreak());
            game.Run();
            //new Bot(Environment.GetEnvironmentVariable("TOKEN"));
        }
    }
}
