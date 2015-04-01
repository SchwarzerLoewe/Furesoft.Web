using System.Collections.Generic;

namespace Furesoft.Web.Internal.HtAccess
{
    public class Block : ICommand
    {
        public string Name { get; set; }

        public string Argument;

        public List<Directive> Directives = new List<Directive>();
    }
}