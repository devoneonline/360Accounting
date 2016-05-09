using _360Accounting.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class HomeController : AsyncController
    {
        public ActionResult Index()
        {
            if (AuthenticationHelper.UserRole == UserRoles.SuperAdmin.ToString())
                return RedirectToAction("Index", "Company");

            SessionHelper.SOBId = UserSetofBookHelper.GetDefaultSOB() == null ? 0 : UserSetofBookHelper.GetDefaultSOB().SOBId;
            SessionHelper.SOBName = SessionHelper.SOBId == 0? "Click here to select Set of Book": SetOfBookHelper.GetSetOfBook(SessionHelper.SOBId.ToString()).Name;
            return View();
        }
    }
}