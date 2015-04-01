using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;

namespace Furesoft.Web.Service.Internal
{
    public class Map : DynamicObject, IEnumerable, IDictionary<string, object>
    {
        private readonly IDictionary<string, object> dict = new Dictionary<string, object>();
        private IList list;

        #region "IDictionary implementation"

        public void Add(KeyValuePair<string, object> item)
        {
            Add(item.Key, item.Value);
        }

        public bool TryGetValue(string key, out object value)
        {
            // TODO: Implement this method
            value = null;
            throw new NotImplementedException();
        }

        public object this[string key]
        {
            get
            {
                return dict[key];
            }
            set
            {
                dict[key] = value;
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                return dict.Keys;
            }
        }

        public ICollection<object> Values
        {
            get
            {
                return dict.Values;
            }
        }

        public void Clear()
        {
            dict.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return dict.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            dict.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return dict.Remove(item);
        }

        public int Count
        {
            get
            {
                return dict.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public bool ContainsKey(string key)
        {
            return dict.ContainsKey(key);
        }

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return dict.GetEnumerator();
        }

        public bool Remove(string key)
        {
            return dict.Remove(key);
        }

#endregion

        public void Add(string key, object value)
        {
            dict.Add(key, value);
            list = null;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            foreach (var name in dict.Keys)
            {
                yield return name;
            }
            foreach (var index in Enumerable.Range(0, dict.Count()))
            {
                yield return index.ToString(CultureInfo.InvariantCulture);
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var name = binder.Name;
            var found = dict.TryGetValue(name, out result);
            if (!found)
            {
                int index;
                if (int.TryParse(name, out index) && (index >= 0) && (index < dict.Count()))
                {
                    if (list == null)
                    {
                        list = dict.ToList();
                    }
                    result = list[index];
                    found = true;
                }
            }
            return found;
        }

        public IEnumerator GetEnumerator()
        {
            return dict.GetEnumerator();
        }
    }
}