using System;
using System.Collections.Generic;
using System.Linq;

namespace Furesoft.Web.ModuleSystem
{
    public class ModuleCache
    {
        private static List<Module> _cache = new List<Module>();

        public static Module GetModule(string name)
        {
            return (from m in _cache where m.Name == name select m).FirstOrDefault();
        }

        public static void Add(Module m)
        {
            _cache.Add(m);
        }

        public static IEnumerable<Module> GetEnumerable()
        {
            return _cache;
        }

    }
}