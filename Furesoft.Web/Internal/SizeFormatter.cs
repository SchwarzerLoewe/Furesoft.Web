using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Furesoft.Web.Internal
{
    public class SizeFormatter
    {
        public static string Format(double len, int decimals)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            while (len >= 1024 && order + 1 < sizes.Length)
            {
                order++;
                len = len / 1024;
            }

            return String.Format("{0:0." + places(decimals) + "} {1}", len, sizes[order]);
        }

        private static string places(int pl)
        {
            string ret = "";

            for (int i = 0; i < pl; i++)
            {
                ret += "#";
            }

            return ret;
        }
    }
}