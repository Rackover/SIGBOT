using System;

namespace SIGBOT
{
    class Program
    {
        static void Main(string[] args)
        {
            new SIGBOT.Components.War.Map().Draw();
            //new Bot(Environment.GetEnvironmentVariable("TOKEN"));
        }
    }
}
