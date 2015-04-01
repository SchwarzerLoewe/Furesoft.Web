// HtmlAgilityPack V1.0 - Simon Mourier <simon underscore mourier at hotmail dot com>
namespace Furesoft.Web.Html
{
    public enum HtmlNodeType
    {
        /// <summary>
        /// The root of a document.
        /// </summary>
        Document,

        /// <summary>
        /// An HTML element.
        /// </summary>
        Element,

        /// <summary>
        /// An HTML comment.
        /// </summary>
        Comment,

        /// <summary>
        /// A text node is always the child of an element or a document node.
        /// </summary>
        Text,
    }
}