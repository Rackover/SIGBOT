using System;

namespace SIGBOT
{
    class Program
    {
        static void Main(string[] args)
        {
            new Bot(Environment.GetEnvironmentVariable("TOKEN"));
        }
    }
}
