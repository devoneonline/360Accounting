using _360Accounting.Core.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Core
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
                return sb.ToString().Substring(0, sb.ToString().LastIndexOf(delimeter) - 1);
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

        public static class Configuration
        {
            public static int GridRows
            {
                get
                {
                    return
                        ConfigurationManager.AppSettings["GridRows"] == null ? 20 : Convert.ToInt32(ConfigurationManager.AppSettings["GridRows"].ToString());
                }
            }
        }

        public static string CodeCombination(CodeCombinition entity, string delimeter)
        {
            return
                Stringize(delimeter, entity.Segment1, entity.Segment2, entity.Segment3, entity.Segment4, entity.Segment5, entity.Segment6, entity.Segment7, entity.Segment8);
        }
    }
}
