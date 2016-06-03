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
        public static string AmountinWords(double amountinDigit)
        {
            int i = (int)amountinDigit;

            if (amountinDigit.ToString().Split('.').Length > 1)
            {
                string decimalPart = amountinDigit.ToString().Split('.')[1];
                string text = NumberToText(i, true, false) + " Point " + DecimalToText(decimalPart);
                return text;
            }
            else
            {
                string text = NumberToText(i, true, false);
                return text;
            }

            //int inputNo = amountinDigit;

            //if (inputNo == 0)
            //    return "Zero";

            //int[] numbers = new int[4];
            //int first = 0;
            //int u, h, t;
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //if (inputNo < 0)
            //{
            //    sb.Append("Minus ");
            //    inputNo = -inputNo;
            //}

            //string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            //"Five " ,"Six ", "Seven ", "Eight ", "Nine "};
            //string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            //"Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
            //string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            //"Seventy ","Eighty ", "Ninety "};
            //string[] words3 = { "Thousand ", "Lakh ", "Crore " };

            //numbers[0] = inputNo % 1000; // units
            //numbers[1] = inputNo / 1000;
            //numbers[2] = inputNo / 100000;
            //numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
            //numbers[3] = inputNo / 10000000; // crores
            //numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

            //for (int i = 3; i > 0; i--)
            //{
            //    if (numbers[i] != 0)
            //    {
            //        first = i;
            //        break;
            //    }
            //}
            //for (int i = first; i >= 0; i--)
            //{
            //    if (numbers[i] == 0) continue;
            //    u = numbers[i] % 10; // ones
            //    t = numbers[i] / 10;
            //    h = numbers[i] / 100; // hundreds
            //    t = t - 10 * h; // tens
            //    if (h > 0) sb.Append(words0[h] + "Hundred ");
            //    if (u > 0 || t > 0)
            //    {
            //        if (h > 0 || i == 0) sb.Append("and ");
            //        if (t == 0)
            //            sb.Append(words0[u]);
            //        else if (t == 1)
            //            sb.Append(words1[u]);
            //        else
            //            sb.Append(words2[t - 2] + words0[u]);
            //    }
            //    if (i != 0) sb.Append(words3[i - 1]);
            //}
            //return sb.ToString().TrimEnd();
        }

        private static string DecimalToText(string decimalPart)
        {
            string[] digits = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            string result = "";
            foreach (char c in decimalPart)
            {
                int i = (int)c - 48;
                if (i < 0 || i > 9) return ""; // invalid number, don't return anything
                result += " " + digits[i];
            }
            return result;
        }

        private static string NumberToText(int number, bool useAnd, bool useArab)
        {
            if (number == 0) return "Zero";

            string and = useAnd ? "and " : ""; // deals with using 'and' separator

            if (number == -2147483648) return "Minus Two Hundred " + and + "Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred " + and + "Forty Eight";

            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }

            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;

                u = num[i] % 10; // ones 
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens

                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i < first) sb.Append(and);

                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }

            string temp = sb.ToString().TrimEnd();

            if (useArab && Math.Abs(number) >= 1000000000)
            {
                int index = temp.IndexOf("Hundred Crore");
                if (index > -1) return temp.Substring(0, index) + "Arab" + temp.Substring(index + 13);
                index = temp.IndexOf("Hundred");
                return temp.Substring(0, index) + "Arab" + temp.Substring(index + 7);
            }
            return temp;
        }

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
