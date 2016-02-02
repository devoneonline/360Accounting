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
            return View();
        }
    }
}