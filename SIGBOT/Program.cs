using SIGBOT.Components.War;
using System;

namespace SIGBOT
{
    class Program
    {
        public static Game game;
        static void Main(string[] args)
        {
            new Bot(Environment.GetEnvironmentVariable("TOKEN"));
        }
    }
}
