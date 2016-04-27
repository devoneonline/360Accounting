using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _360Accounting.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (SessionHelper.SOBId > 0)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "UserSetofBook", action = "SelectSOB" }));
                filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
            }
        }
    }
}