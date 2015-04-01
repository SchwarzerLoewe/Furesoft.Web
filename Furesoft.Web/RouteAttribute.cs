using System;

namespace Furesoft.Web
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RouteAttribute : Attribute
    {
        public RouteAttribute(string route)
        {
            this.Route = route;
        }

        public string Route { get; private set; }

        public Type Type { get; set; }
    }
}