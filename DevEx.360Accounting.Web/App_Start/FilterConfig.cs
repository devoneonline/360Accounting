using System.Web;
using System.Web.Mvc;

namespace DevEx_360Accounting_Web {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}