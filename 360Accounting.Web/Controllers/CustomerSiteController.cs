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

        public ActionResult Edit(long customerId, long? id)
        {
            CustomerSiteModel model;
            if (id != null)
                model = new CustomerSiteModel(service.GetSingle(id.Value.ToString(), AuthenticationHelper.User.CompanyId));
            else
            {
                model = new CustomerSiteModel();
                model.CustomerId = customerId;
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

        #region Private Methods
        private CustomerSite mapModel(CustomerSiteModel model)
        {
            if (model == null) return null;
            CustomerSite entity = new CustomerSite();
            entity.CustomerId = model.CustomerId;
            entity.CodeCombinationId = model.CodeCombinationId;
            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.SiteAddress = model.SiteAddress;
            entity.SiteContact = model.SiteContact;
            entity.SiteName = model.SiteName;
            entity.StartDate = model.StartDate;
            entity.TaxCodeId = model.TaxCodeId;

            if (model.Id == 0)
            {
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }
        #endregion

    }
}