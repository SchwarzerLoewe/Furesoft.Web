namespace Furesoft.Web.html
{
    partial class Trace
    {
        partial void WriteLineIntern(string message, string category)
        {
            System.Diagnostics.Debug.WriteLine(message, category);
        }
    }
}