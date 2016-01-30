using _360Accounting.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class CalendarController : Controller
    {
        //private ICalendarService service;
        private ISetOfBookService sobService;

        public CalendarController()
        {
                
        }

        public ActionResult Index()
        {

            return View();
        }
    }
}