using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
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
        public ActionResult Index(string message = "")
        {
            ViewBag.ErrorMessage = message;
            CompanyListModel model = new CompanyListModel();
            model.Companies = CompanyHelper.GetCompanies();
            AuthenticationHelper.CompanyList = model.Companies;
            return View(model);
        }

        public ActionResult Create()
        {
            return View("Edit", new CompanyModel());
        }

        public ActionResult Edit(string id)
        {
            return View(CompanyHelper.GetCompany(id));
        }

        [HttpPost]
        public ActionResult Edit(CompanyModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string result = CompanyHelper.SaveCompany(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }                
            }

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            try
            {
                CompanyHelper.DeleteCompany(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { message = ex.Message });
            }
        }

        public ActionResult CompanyListPartial()
        {
            return PartialView("_List", CompanyHelper.GetCompanies());
        }
    }
}