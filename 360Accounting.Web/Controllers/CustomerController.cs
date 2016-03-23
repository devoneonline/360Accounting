using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        [HttpPost]
        public ActionResult Edit(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                //Customer duplicateRecord = service.GetSingle(model.Id.ToString(), AuthenticationHelper.User.CompanyId);
                //if (duplicateRecord == null)
                //{
                string result = CustomerHelper.SaveCustomer(model);
                return RedirectToAction("Index");
                //}
                //else
                //{
                //    ModelState.AddModelError("Error", "Customer Already exists.");
                //}
            }
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            return View(CustomerHelper.GetCustomer(id));
        }

        public ActionResult Delete(string id)
        {
            CustomerHelper.DeleteCustomer(id);
            return RedirectToAction("Index");
        }

        public ActionResult CustomerListPartial()
        {
            return PartialView("_List", CustomerHelper.GetCustomers());
        }

        //[HttpPost]
        //public ActionResult Create(CustomerModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Customer duplicateRecord = service.GetSingle(model.Id.ToString(), AuthenticationHelper.User.CompanyId);
        //        //if (duplicateRecord == null)
        //        //{
        //            string result = CustomerHelper.SaveCustomer(model);
        //            return RedirectToAction("Index");
        //        //}
        //        //else
        //        //{
        //        //    ModelState.AddModelError("Error", "Customer Already exists.");
        //        //}
        //    }
        //    return View(model);
        //}

        public ActionResult Create()
        {
            return View("Edit", new CustomerModel());
        }

        // GET: Customer
        public ActionResult Index()
        {
            CustomerListModel model = new CustomerListModel();
            model.Customers = CustomerHelper.GetCustomers();
            return View(model);
        }
    }
}