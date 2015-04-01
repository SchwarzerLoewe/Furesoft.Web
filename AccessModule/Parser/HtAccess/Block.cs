using System.Collections.Generic;

namespace AccessModule.Parser.HtAccess
{
    public class Block : ICommand
    {
        public string Name { get; set; }

        public string Argument;

        public List<Directive> Directives = new List<Directive>();
    }
}