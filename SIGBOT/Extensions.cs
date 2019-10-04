using System;
using System.Collections.Generic;
using System.Text;

namespace SIGBOT
{
    public static class Extensions
    {
        public static string Format(this string str, params string[] args)
        {
            return string.Format(str, args);
        }
    }
}
