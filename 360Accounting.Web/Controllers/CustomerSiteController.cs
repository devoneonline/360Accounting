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
    public class CustomerSiteController : BaseController
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

        public ActionResult Index(long Id, string message="")//CustomerId
        {
            ViewBag.ErrorMessage = message;
            ViewBag.CustomerId = Id;
            ViewBag.CustomerName = CustomerHelper.GetCustomer(Id.ToString()).CustomerName;
            List<CustomerSiteViewModel> model = new List<CustomerSiteViewModel>();
            model = CustomerHelper.GetCustomerSites(Id);
            return View(model);
        }

        public ActionResult Edit(long customerId, long? id)
        {
            ViewBag.CustomerName = CustomerHelper.GetCustomer(customerId.ToString()).CustomerName;

            CustomerSiteModel model;
            if (id != null)
            {
                model = CustomerHelper.GetCustomerSite(id.Value.ToString());
                CodeCombinitionCreateViewModel codeCombination = CodeCombinationHelper.GetCodeCombination(model.CodeCombinationId.ToString());

                model.CodeCombinationString = Utility.Stringize(".", codeCombination.Segment1, codeCombination.Segment2, codeCombination.Segment3,
                    codeCombination.Segment4, codeCombination.Segment5, codeCombination.Segment6, codeCombination.Segment7, codeCombination.Segment8);
            }

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

            //model.CodeCombination = codeCombinationService.GetAllCodeCombinitionView(AuthenticationHelper.CompanyId.Value)
            //        .Select(x => new SelectListItem
            //        {
            //            Text = x.CodeCombinitionCode,
            //            Value = x.Id.ToString()
            //        }).ToList();
            //model.CodeCombinationId = model.CodeCombination.Any() ? Convert.ToInt64(model.CodeCombination.First().Value) : 0;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CustomerSiteModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string result = "";
                    CustomerModel customer = CustomerHelper.GetCustomer(model.CustomerId.ToString());
                    if ((model.StartDate >= customer.StartDate && model.EndDate <= customer.EndDate) ||
                        (model.StartDate == null && customer.StartDate == null ||
                        model.EndDate == null && customer.EndDate == null))
                    {
                        result = CustomerHelper.SaveCustomerSite(model);
                        return RedirectToAction("Index", new { Id = model.CustomerId });
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Site Dates should be within the range of Customer Dates.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }
            }

            model.TaxCode = taxService.GetAll(AuthenticationHelper.CompanyId.Value)
                .Select(x => new SelectListItem
                {
                    Text = x.TaxName,
                    Value = x.Id.ToString()
                }).ToList();
            model.TaxId = model.TaxCode.Any() ? Convert.ToInt64(model.TaxCode.First().Value) : 0;

            return View(model);
        }

        public ActionResult Delete(string id, long CustomerId)
        {
            try
            {
                CustomerHelper.DeleteCustomerSite(id);
                return RedirectToAction("Index", new { Id = CustomerId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { Id = CustomerId, message=ex.Message });
            }
        }

        public ActionResult CustomerSiteListPartial()
        {
            IEnumerable<CustomerSiteModel> list = CustomerHelper.GetCustomerSites();
            return PartialView("_List", list);
        }
    }
}