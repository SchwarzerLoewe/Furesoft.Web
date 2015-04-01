using System;
using System.Collections.Generic;
using System.IO;
using Furesoft.Web.Internal;

namespace AccessModule
{
    public class Access
    {
        private readonly string dir;

        public string AuthType;
        public string AuthName;
        public string DirectoryIndex;
        public string AuthUserFile;
        public List<User> Users = new List<User>();
        public Dictionary<string, string> ErrorDocument = new Dictionary<string, string>();
        public Dictionary<string, string> RewriteRule = new Dictionary<string, string>();
        public Dictionary<string, string> Options = new Dictionary<string, string>();
        public Dictionary<string, string> AddType = new Dictionary<string, string>();
        public Dictionary<string, string> Redirect = new Dictionary<string, string>();
        public bool RewriteEngine;
        public List<string> Deny;
        public List<string> IndexIgnore;

        public Access(string dir)
        {
            this.dir = dir;

            Deny = new List<string>();
            AuthName = "";
            AuthType = "";
            AuthUserFile = "";
            RewriteEngine = false;
        }

        public static bool HasAccess(string dir)
        {
            try
            {
                foreach (var item in Directory.GetFiles(dir))
                {
                    if (Path.GetFileName(item) == ".htaccess")
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return true;
            }
        }

        public void ReadUserFile()
        {
            try
            {
                //derName:$apr1$m0OaZVp0$9OHApAf65z24vNUZts8Zz1
                foreach (var line in File.ReadAllText(dir + "\\" + AuthUserFile).Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var t = line.Trim().Split(':');
                    var user = new User();
                    user.Username = t[0];
                    user.Password = t[1];

                    Users.Add(user);
                }
            }
            catch
            {

            }
        }
    }
}