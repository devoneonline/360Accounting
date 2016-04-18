﻿using _360Accounting.Core;
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
    public class CustomerSiteController : Controller
    {
        //private ICustomerSiteService service;
        private ICodeCombinitionService codeCombinationService;
        private ITaxService taxService;

        public CustomerSiteController()
        {
            //service = IoC.Resolve<ICustomerSiteService>("CustomerSiteService");
            codeCombinationService = IoC.Resolve<ICodeCombinitionService>("CodeCombinitionService");
            taxService = IoC.Resolve<ITaxService>("TaxService");
        }

        public ActionResult Index(long Id)//CustomerId
        {
            ViewBag.CustomerId = Id;
            List<CustomerSiteViewModel> model = new List<CustomerSiteViewModel>();
            model = CustomerHelper.GetCustomerSites(Id);
            return View(model);
        }

        public ActionResult Edit(long customerId, long? id)
        {
            CustomerSiteModel model;
            if (id != null)
                model = CustomerHelper.GetCustomerSite(id.Value.ToString());
            else
            {
                model = new CustomerSiteModel();
                model.CustomerId = customerId;
            }

            model.TaxCode = taxService.GetAll(AuthenticationHelper.CompanyId.Value)
                .Select(x => new SelectListItem
                {
                    Text = x.TaxName,
                    Value = x.Id.ToString()
                }).ToList();
            model.TaxId = model.TaxCode.Any() ? Convert.ToInt64(model.TaxCode.First().Value) : 0;

            model.CodeCombination = codeCombinationService.GetAllCodeCombinitionView(AuthenticationHelper.CompanyId.Value)
                    .Select(x => new SelectListItem
                    {
                        Text = x.CodeCombinitionCode,
                        Value = x.Id.ToString()
                    }).ToList();
            model.CodeCombinationId = model.CodeCombination.Any() ? Convert.ToInt64(model.CodeCombination.First().Value) : 0;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CustomerSiteModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.StartDate != null && model.StartDate > model.EndDate)
                    {
                        ModelState.AddModelError("Error", "Start Date cannot be greater than End Date.");
                    }
                    else
                    {
                        string result = "";
                        result = CustomerHelper.SaveCustomerSite(model);
                        return RedirectToAction("Index", new { Id = model.CustomerId });
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }                
            }
            return View(model);
        }

        public ActionResult Delete(string id, long CustomerId)
        {
            CustomerHelper.DeleteCustomerSite(id);
            return RedirectToAction("Index", new { Id = CustomerId });
        }

        public ActionResult CustomerSiteListPartial()
        {
            IEnumerable<CustomerSiteModel> list = CustomerHelper.GetCustomerSites();
            return PartialView("_List", list);
        }
    }
}