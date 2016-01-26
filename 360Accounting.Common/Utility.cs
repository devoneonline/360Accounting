using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Common
{
    public sealed class Utility
    {
        public static string Stringize(string delimeter, params string[] value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in value)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    sb.Append(s).Append(" ").Append(delimeter).Append(" ");
                }
            }

            if (sb.ToString().Length > 0)
            {
                return sb.ToString().Substring(0, sb.ToString().LastIndexOf("-") - 1);
            }
            else
            {
                return sb.ToString();
            }                
        }

        public static string Stringize(string delimeter, params int?[] value)
        {
            return
                Stringize(delimeter, value.Where(x => x.HasValue).Select(x => x.ToString()).ToArray());
        }
    }
}
