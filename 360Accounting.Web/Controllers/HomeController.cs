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
            //Need to confirm..
            SessionHelper.SOBId = UserSetofBookHelper.GetDefaultSOB() == null ? 0 : UserSetofBookHelper.GetDefaultSOB().SOBId;
            SessionHelper.SOBName = SessionHelper.SOBId == 0? "Select Default SOB": SetOfBookHelper.GetSetOfBook(SessionHelper.SOBId.ToString()).Name;
            return View();
        }
    }
}