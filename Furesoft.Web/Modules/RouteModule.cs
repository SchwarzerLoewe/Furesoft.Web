using System.Collections.Generic;
using System.Reflection;

namespace Furesoft.Web.Modules
{
    public class RouteModule
    {
        public static IEnumerable<RouteAttribute> FindRoutes(Assembly ass)
        {
            foreach (var a in ass.GetTypes())
            {
                var ca = a.GetCustomAttributes(typeof(RouteAttribute), true);

                if (ca.Length == 1 | ca != null)
                {
                    foreach (var item in ca)
                    {
                        var i = (RouteAttribute)item;
                        i.Type = a;

                        yield return i;
                    }
                }
            }
        }
    }
}