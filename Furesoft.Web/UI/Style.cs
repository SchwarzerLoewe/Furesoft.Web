using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Furesoft.Web.UI
{
    public class Style : DynamicObject
    {
        private Dictionary<string, string> data = new Dictionary<string, string>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (data.ContainsKey(binder.Name))
                data[binder.Name] = value.ToString();
            else
                Append(binder.Name, value.ToString());

            return base.TrySetMember(binder, value);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (data.ContainsKey(binder.Name))
            {
                result = data[binder.Name];

                return true;
            }

            result = null;
            return false;
        }

        public void Append(string name, string value)
        {
            data.Add(name, value);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var kv in data)
            {
                sb.Append(kv.Key + ": " + kv.Value + ";");
            }

            return sb.ToString();
        }
    }
}