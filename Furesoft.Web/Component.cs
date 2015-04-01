using System;
using System.Collections.Generic;
using System.Reflection;

namespace Furesoft.Web
{
    public class Component
    {
        private Dictionary<string, Type> types = new Dictionary<string, Type>();

        public dynamic New(string name)
        {
            return Activator.CreateInstance(types[name]);
        }

        public static Component Open(string ass)
        {
            var asss = Assembly.LoadFile(ass);
            var c = new Component();

            foreach (var i in asss.GetTypes())
            {
                c.types.Add(i.Name, i);
            }

            return c;
        }
    }
}