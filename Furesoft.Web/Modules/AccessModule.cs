using Furesoft.Web.Internal;

namespace Furesoft.Web.Modules
{
    public static class AccessModule
    {
        private static Access _ac = new Access("");

        public static void Init(Access ac)
        {
            _ac = ac;
        }

        public static bool IsBlocked(string ip)
        {
            if (_ac.Deny.Contains("all"))
            {
                return true;
            }
            else
            {
                return _ac.Deny.Contains(ip);
            }
        }
    }
}