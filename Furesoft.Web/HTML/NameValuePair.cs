// HtmlAgilityPack V1.0 - Simon Mourier <simon underscore mourier at hotmail dot com>
namespace Furesoft.Web.html
{
    internal class NameValuePair
    {
        #region Fields

        internal readonly string Name;
        internal string Value;

        #endregion Fields

        #region Constructors

        internal NameValuePair()
        {
        }

        internal NameValuePair(string name)
            :
                this()
        {
            Name = name;
        }

        internal NameValuePair(string name, string value)
            :
                this(name)
        {
            Value = value;
        }

        #endregion Constructors
    }
}