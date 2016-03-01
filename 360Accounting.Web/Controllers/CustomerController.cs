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
        private ICustomerService service;

        #region Private Methods
        private Customer mapModel(CustomerModel model)
        {
            return new Customer
            {
                Address = model.Address,
                CompanyId = AuthenticationHelper.User.CompanyId,
                ContactNo = model.ContactNo,
                CreateDate = DateTime.Now,
                CustomerName = model.CustomerName,
                EndDate = model.EndDate,
                Id = model.Id,
                StartDate = model.StartDate,
                UpdateDate = DateTime.Now
            };
        }
        #endregion

        public CustomerController()
        {
            service = IoC.Resolve<ICustomerService>("CustomerService");
        }

        [HttpPost]
        public ActionResult Edit(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                //Customer duplicateRecord = service.GetSingle(model.Id.ToString(), AuthenticationHelper.User.CompanyId);
                //if (duplicateRecord == null)
                //{
                string result = service.Update(mapModel(model));
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
            CustomerModel model = new CustomerModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
            return RedirectToAction("Index");
        }

        public ActionResult CustomerListPartial()
        {
            IEnumerable<CustomerModel> list = service
                .GetAll(AuthenticationHelper.User.CompanyId)
                .Select(a => new CustomerModel(a)).ToList();
            return PartialView("_List", list);
        }

        [HttpPost]
        public ActionResult Create(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                //Customer duplicateRecord = service.GetSingle(model.Id.ToString(), AuthenticationHelper.User.CompanyId);
                //if (duplicateRecord == null)
                //{
                    string result = service.Insert(mapModel(model));
                    return RedirectToAction("Index");
                //}
                //else
                //{
                //    ModelState.AddModelError("Error", "Customer Already exists.");
                //}
            }
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new CustomerModel());
        }

        // GET: Customer
        public ActionResult Index()
        {
            CustomerListModel model = new CustomerListModel();
            IEnumerable<Customer> list = service.GetAll(AuthenticationHelper.User.CompanyId);
            model.Customers = list.Select(a => new CustomerModel(a)).ToList();
            return View(model);
        }
    }
}