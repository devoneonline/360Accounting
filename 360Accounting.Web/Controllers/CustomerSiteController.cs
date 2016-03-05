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
    public class CustomerSiteController : Controller
    {
        private ICustomerSiteService service;
        private ICodeCombinitionService codeCombinationService;
        private ITaxService taxService;

        #region Private Methods
        private CustomerSite mapModel(CustomerSiteModel model)
        {
            return new CustomerSite
            {
                CustomerId = model.CustomerId,
                CodeCombinationId = model.CodeCombinationId,
                EndDate = model.EndDate,
                Id = model.Id,
                SiteAddress = model.SiteAddress,
                SiteContact = model.SiteContact,
                SiteName = model.SiteName,
                StartDate = model.StartDate,
                TaxCodeId = model.TaxCodeId,
                UpdateDate = DateTime.Now,
                CreateDate = DateTime.Now //TODO: these should be generic..//TODO: this should be generic..
            };
        }
        #endregion

        public CustomerSiteController()
        {
            service = IoC.Resolve<ICustomerSiteService>("CustomerSiteService");
            codeCombinationService = IoC.Resolve<ICodeCombinitionService>("CodeCombinitionService");
            taxService = IoC.Resolve<ITaxService>("TaxService");
        }

        public ActionResult Index(long Id)//CustomerId
        {
            ViewBag.CustomerId = Id;
            List<CustomerSiteViewModel> model = new List<CustomerSiteViewModel>();
            IEnumerable<CustomerSiteView> list;
            list = service.GetAllbyCustomerId(Id);
            model = list.Select(a => new CustomerSiteViewModel(a)).ToList();
            return View(model);
        }

        public ActionResult Edit(long? Id, long CustomerId)
        {
            CustomerSiteModel model;
            if (Id != null)
                model = new CustomerSiteModel(service.GetSingle(Id.Value.ToString(), AuthenticationHelper.User.CompanyId));
            else
            {
                model = new CustomerSiteModel();
                model.CustomerId = CustomerId;
            }

            model.TaxCode = taxService.GetAll(AuthenticationHelper.User.CompanyId)
                .Select(x => new SelectListItem
                {
                    Text = x.TaxName,
                    Value = x.Id.ToString()
                }).ToList();
            model.CodeCombination = codeCombinationService.GetAllCodeCombinitionView(AuthenticationHelper.User.CompanyId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.CodeCombinitionCode,
                        Value = x.Id.ToString()
                    }).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CustomerSiteModel model)
        {
            if (ModelState.IsValid)
            {
                string result = "";
                if (model.Id > 0)
                    result = service.Update(mapModel(model));
                else
                    result = service.Insert(mapModel(model));

                return RedirectToAction("Index", new { Id = model.CustomerId });
            }
            return View(model);
        }

        public ActionResult Delete(string id, long CustomerId)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
            return RedirectToAction("Index", new { Id = CustomerId });
        }

        public ActionResult CustomerSiteListPartial()
        {
            IEnumerable<CustomerSiteModel> list = service
                .GetAll(AuthenticationHelper.User.CompanyId)
                .Select(a => new CustomerSiteModel(a)).ToList();
            return PartialView("_List", list);
        }

        public ActionResult Create()
        {
            return View(new CustomerSiteModel());
        }
    }
}