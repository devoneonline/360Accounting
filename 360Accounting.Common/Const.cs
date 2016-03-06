using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360Accounting.Common
{
    public static class Const
    {
        public const string DATE_FORMAT = "MM/dd/yyyy";
        
        public static DateTime CurrentDate = DateTime.Now;

        public static DateTime FromDate = DateTime.Now.AddDays(-7);

        public static DateTime ToDate = DateTime.Now.AddDays(7);
    }
}
