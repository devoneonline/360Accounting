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
    public class CustomerController : BaseController
    {
        [HttpPost]
        public ActionResult Edit(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.StartDate != null && model.StartDate > model.EndDate)
                {
                    ModelState.AddModelError("Error", "Start Date cannot be greater than End Date.");
                }
                else
                {
                    string result = CustomerHelper.SaveCustomer(model);
                    return RedirectToAction("Index");
                }
                
            }
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            return View(CustomerHelper.GetCustomer(id));
        }

        public ActionResult Delete(string id)
        {
            try
            {
                CustomerHelper.DeleteCustomer(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { message = ex.Message });
            }
        }

        public ActionResult CustomerListPartial()
        {
            return PartialView("_List", CustomerHelper.GetCustomers());
        }
        
        public ActionResult Create()
        {
            return View("Edit", new CustomerModel());
        }

        public ActionResult Index(string message = "")
        {
            ViewBag.ErrorMessage = message;
            CustomerListModel model = new CustomerListModel();
            model.Customers = CustomerHelper.GetCustomers();
            return View(model);
        }
    }
}