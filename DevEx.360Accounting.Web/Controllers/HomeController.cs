using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevEx_360Accounting_Web.Models;

namespace DevEx_360Accounting_Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }


        //public ActionResult Index()
        //{
        //    // DXCOMMENT: Pass a data model for GridView
            
        //    return View(NorthwindDataProvider.GetCustomers());    
        //}
        
        //public ActionResult GridViewPartialView() 
        //{
        //    // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
        //    return PartialView("GridViewPartialView", NorthwindDataProvider.GetCustomers());
        //}
    
    }
}