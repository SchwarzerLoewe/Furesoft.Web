using System;
using Furesoft.Web.Internal.TemplateEngine;

namespace Furesoft.Web.Internal.TemplateEngine
{
    public class StringArgument : IArgument
    {
        private readonly string value;

        public StringArgument(string value)
        {
            this.value = value;
        }

        public string GetKey()
        {
            return null;
        }

        public object GetValue(Scope keyScope, Scope contextScope)
        {
            return value;
        }
    }
}
