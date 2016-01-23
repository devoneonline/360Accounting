using _360Accounting.Core.Entities;
using _360Accounting.Core.IService;
using _360Accounting.Data.Repositories;
using _360Accounting.Service.Services;
using _360Accounting.Web.Models;
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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CompanyModel model)
        {
            if (ModelState.IsValid)
            {
                string result = service.Insert(MapModel(model));
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private Company MapModel(CompanyModel model)            ////TODO: this should be done in service will discuss later - FK
        {
            return new Company
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}