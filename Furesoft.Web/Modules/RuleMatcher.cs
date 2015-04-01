using Furesoft.Web.Internal;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Furesoft.Web.Modules
{
    public class RuleMatcher
    {
        public static Match IsMatch(string input, Dictionary<string, string> rules, Access ac)
        {
            if (ac.RewriteEngine)
            {
                foreach (var r in rules)
                {
                    if (Regex.IsMatch(input, r.Key))
                    {
                        return Regex.Match(input, r.Key);
                    }
                }
            }
            return null;
        }
    }
}