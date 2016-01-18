using _360Accounting.Core;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
////using _360Accounting.Core.IService;
////using _360Accounting.Data.Repositories;
////using _360Accounting.Service.Services;
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
        private IFeatureService service;

        public HomeController()
        {
            service = new FeatureService(new FeatureRepository());
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}