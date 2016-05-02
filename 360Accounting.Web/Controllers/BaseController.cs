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
        protected override void OnException(ExceptionContext filterContext)
        {
            TempData["ErrorMessage"] = filterContext.Exception.Message;
            TempData["ErrorSource"] = filterContext.Exception.GetBaseException().Source;

            TempData["LastURL"] = Request.UrlReferrer.AbsolutePath;

            ViewResult vr = new ViewResult();
            vr.ViewName = "_OnException";
            filterContext.Result = vr;

            filterContext.ExceptionHandled = true;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (SessionHelper.SOBId > 0)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                if (Request.Url.Segments[1].ToUpper() == "SETOFBOOK" || Request.Url.Segments[1].ToUpper() == "SETOFBOOK/")
                    base.OnActionExecuting(filterContext);
                else
                {
                    TempData["LastURL"] = Request.Url.PathAndQuery;

                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "UserSetofBook", action = "SelectSOB" }));
                    filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
                }
            }
        }

        public ActionResult ViewErrorPopup()
        {
            TempData["LastURL"] = TempData["LastURL"];
            return PartialView("_Exception");
        }
    }
}