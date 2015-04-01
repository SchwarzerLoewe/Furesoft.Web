using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Furesoft.Web.ModuleSystem
{
    public class Module
    {
        public string Name { get; set; }

        public Assembly Assembly { get; set; }

        public IModule CreateInstance()
        {
            return (IModule)Activator.CreateInstance((from m in Assembly.GetTypes() where m.IsAssignableFrom(typeof(IModule)) select m).FirstOrDefault());
        }

        public static Module FromRuntime(object obj)
        {
            return new Module { Name = obj.GetType().Name, Assembly = obj.GetType().Assembly };
        }

        public static Module FromFile(string filename)
        {
            var m = new Module(); 
            m.Assembly = Assembly.Load(File.ReadAllBytes(filename));
            m.Name = m.Assembly.GetName().Name;

            return m;
        }
        
        public static Module FromMemory(byte[] buff)
        {
            var m = new Module();
            m.Assembly = Assembly.Load(buff);
            m.Name = m.Assembly.GetName().Name;

            return m;
        }
    }
}