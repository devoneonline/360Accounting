using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using DevEx_360Accounting_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevEx_360Accounting_Web.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private ICompanyService service;

        public CompanyController()
        {
            service = IoC.Resolve<ICompanyService>("CompanyService");
        }

        public ActionResult Index()
        {
            CompanyListModel model = new CompanyListModel();
            var list = service.GetAll(AuthenticationHelper.User.CompanyId);
            model.Companies = list.Select(a => new CompanyModel(a)).ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new CompanyModel());
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

        public ActionResult Edit(string id)
        {
            CompanyModel model = new CompanyModel(service.GetSingle(id,AuthenticationHelper.User.CompanyId));
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CompanyModel model)
        {
            if (ModelState.IsValid)
            {
                string result = service.Update(MapModel(model));
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            service.Delete(id,AuthenticationHelper.User.CompanyId);
            return RedirectToAction("Index");
        }

        private Company MapModel(CompanyModel model)            ////TODO: this should be done in service will discuss later - FK
        {
            return new Company
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public ActionResult CompanyListPartial()
        {
            var list = service.GetAll(AuthenticationHelper.User.CompanyId);
            IEnumerable<CompanyModel> companyList = list.Select(a => new CompanyModel(a));
            return PartialView("_List", companyList);
        }
    }
}