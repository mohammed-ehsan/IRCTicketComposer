using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Helpdesk.Core
{
    public static class Helpers
    {
        public static string TryGetValue(this string[] data, int index)
        {
            if (data.Count() - 1 >= index)
            {
                return data[index];
            }
            return string.Empty;
        }

        public static string[] ToStringArray(this object[] data)
        {
            return data.Select((i) => { return i.ToString(); }).ToArray();
        }
    }
}
