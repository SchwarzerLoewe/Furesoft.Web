using System;
using Furesoft.Web.Internal.TemplateEngine;

namespace Furesoft.Web.Internal.TemplateEngine
{
    public class NumberArgument : IArgument
    {
        private readonly decimal value;

        public NumberArgument(decimal value)
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
