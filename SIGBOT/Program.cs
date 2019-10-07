using SIGBOT.Components.War;
using System;

namespace SIGBOT
{
    class Program
    {
        public static Game game;
        static void Main(string[] args)
        {
            for (var i = 1; i < 500; i++)
            {
                game.ReadFromDisk();
                if (game.Advance()) break;
                game.WriteToDisk();
            }
            
            new Bot(Environment.GetEnvironmentVariable("TOKEN"));
        }
    }
}
