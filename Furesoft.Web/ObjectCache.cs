using System;
using System.Collections.Generic;

namespace Furesoft.Web
{
    public static class ObjectCache
    {
        private static Dictionary<string, object> _cache = new Dictionary<string, object>();

        public static T Get<T>()
        {
            return (T)_cache[typeof(T).FullName];
        }

        public static void Store<T>()
            where T : class, new()
        {
            Store(typeof(T).FullName, new T());
        }

        public static void Store(string name, object obj)
        {
            _cache.Add(name, obj);
        }
    }
}