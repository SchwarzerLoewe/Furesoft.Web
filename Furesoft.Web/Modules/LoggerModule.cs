using System;
using System.IO;

namespace Furesoft.Web.Modules
{
    public static class LoggerModule
    {
        private static StreamWriter sw;

        public static void Init()
        {
            sw = new StreamWriter(new FileStream(".log", FileMode.Append));
        }

        public static void Log(Exception e)
        {
            Log(e.Message);
        }

        public static void Log(string message)
        {
            sw.WriteLine(string.Format("{0}: {1}", DateTime.Now, message));
            Console.WriteLine(string.Format("{0}: {1}", DateTime.Now, message));
            sw.Flush();
        }

        public static void Close()
        {
            sw.Close();
        }
    }
}