using System;
using System.Collections.Generic;

namespace Furesoft.Web.Service
{
    public abstract class IService
    {
        public IDictionary<string, object> Args { get; set; }
        public abstract string Name { get; }
        public abstract byte[] Start();
    }
}