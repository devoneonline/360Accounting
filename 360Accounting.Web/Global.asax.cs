using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace _360Accounting.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.DefaultBinder = new DevExpress.Web.Mvc.DevExpressEditorsBinder();
        }


        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //if (User.Identity.IsAuthenticated )
            //{
            //    var a= User.Identity.Name;
            //}
        }

        protected void Application_AuthorizeRequest(object sender,EventArgs e)
        {

        }
    }
}
