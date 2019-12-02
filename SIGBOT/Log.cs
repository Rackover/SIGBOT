using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SIGBOT
{
    public static class Log
    {
        public static void Trace(object line)
        {
            Output(line, ConsoleColor.Gray);
        }

        public static void Info(object line)
        {
            Output(line, ConsoleColor.Green);
        }

        public static void Warn(object line)
        {
            Output(line, ConsoleColor.Yellow);
        }

        public static void Err(object line)
        {
            Output(line, ConsoleColor.Red);
        }   

        static void Output(object line, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("{0} - {1}", DateTime.UtcNow.ToString("G", new CultureInfo("fr-FR")), line.ToString());
        }
    }
}
