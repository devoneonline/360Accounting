using System.Collections.Generic;

namespace _360Accounting.Common
{
    public class ExceptionHelper
    {
        private static Dictionary<string, string> errorSummary;

        static ExceptionHelper()
        {
            errorSummary = new Dictionary<string, string>();
        }

        public static void AddError(string key, string errorMessage)
        {
            errorSummary.Add(key, errorMessage);
        }

        public static Dictionary<string, string> ShowErrors()
        {
            return errorSummary;
        }
    }
}
