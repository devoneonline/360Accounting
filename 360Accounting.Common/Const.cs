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

        public const string DATE_FORMAT_2 = "MMM-dd-yyyy";
        
        public static DateTime CurrentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        //public static DateTime StartDate = CurrentDate.AddYears(-1);

        //public static DateTime EndDate = CurrentDate.AddDays(7);

        public static DateTime StartDate = new DateTime(CurrentDate.Year, 1, 1);
        public static DateTime EndDate = new DateTime(CurrentDate.Year, 12, 31);
    }
}