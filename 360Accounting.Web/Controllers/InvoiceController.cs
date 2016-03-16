using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class InvoiceController : Controller
    {
        public ActionResult Index(InvoiceListModel model)
        {
            return View();
        }
    }
}