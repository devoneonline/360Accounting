using _360Accounting.Core;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _360Accounting.Core.Entities;

namespace _360Accounting.Web.Controllers
{
    public class CurrencyController : Controller
    {
        private ICurrencyService service;
        private ISetOfBookService sobService;

        public CurrencyController()
        {
            service = new CurrencyService(new CurrencyRepository());
            sobService = new SetOfBookService(new SetOfBookRepository());
        }

        public ActionResult Index(CurrencyListModel model)
        {
            if (model.SetOfBooks == null)
            {
                model.SetOfBooks = sobService
                    .GetByCompanyId(AuthenticationHelper.User.CompanyId)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
            }

            if (model.SOBId != 0 || model.SetOfBooks != null)
            {
                model.Currencies = getCurrencyList(model);
            }
                
            return View(model);
        }

        public ActionResult Create(long sobId)
        {
            CurrencyViewModel model = new CurrencyViewModel();
            model.SOBId = sobId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CurrencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CompanyId = AuthenticationHelper.User.CompanyId;
                string result = service.Insert(mapModel(model));    ////TODO: mapper should be in service
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(string id)
        {
            CurrencyViewModel model =
                new CurrencyViewModel(service.GetSingle(id));
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CurrencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CompanyId = AuthenticationHelper.User.CompanyId;
                string result = service.Update(mapModel(model));
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            service.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult GetCurrencyList(long sobId)
        {
            CurrencyListModel model = new CurrencyListModel();
            model.SOBId = sobId;
            model.Currencies = getCurrencyList(model);
            return PartialView("_List", model);
        }

        #region Private Methods
        private List<CurrencyViewModel> getCurrencyList(CurrencyListModel model)
        {
            return service.GetAll(AuthenticationHelper.User.CompanyId, model.SOBId != 0 ? model.SOBId : Convert.ToInt64(model.SetOfBooks.First().Value), model.SearchText, true, model.Page, model.SortColumn, model.SortDirection)
                .Select(x => new CurrencyViewModel(x)).ToList();
        }

        private Currency mapModel(CurrencyViewModel model)            ////TODO: this should be done in service will discuss later - FK
        {
            return new Currency
            {
                Id = model.Id,
                CompanyId = model.CompanyId,
                CreateDate = DateTime.Now,
                CurrencyCode = model.CurrencyCode,
                Name = model.Name,
                Precision = model.Precision,
                SOBId = model.SOBId,
                UpdateDate = DateTime.Now                
            };
        }
        #endregion
    }
}