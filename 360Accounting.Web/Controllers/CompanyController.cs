using _360Accounting.Core.IService;
using _360Accounting.Data.Repositories;
using _360Accounting.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private ICompanyService service;

        public CompanyController()
        {
            service = new CompanyService(new CompanyRepository());
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}