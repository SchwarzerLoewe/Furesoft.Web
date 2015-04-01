using System.Reflection;

namespace Furesoft.Web
{
    public class AssemblyPageResolver
    {
        public static Page Resolve(Assembly ass)
        {
            if (ass != null)
            {
                foreach (var a in ass.GetTypes())
                {
                    if (a.BaseType.Name == "Page")
                    {
                        return (Page)ass.CreateInstance(a.FullName);
                    }
                }
            }

            return null;
        }
    }
}