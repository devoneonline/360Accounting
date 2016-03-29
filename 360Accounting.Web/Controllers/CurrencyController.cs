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
    [Authorize]
    public class CurrencyController : Controller
    {
        public ActionResult Index()
        {
            var model = new CurrencyListModel();
            if (model.SetOfBooks == null)
            {
                model.SetOfBooks = SetOfBookHelper.GetSetOfBooks()
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
            }

            model.SOBId = Convert.ToInt64(model.SetOfBooks.FirstOrDefault().Value);
            return View(model);
        }

        public ActionResult Create(long sobId)
        {
            CurrencyViewModel model = new CurrencyViewModel();
            model.SOBId = sobId;
            return View("Edit", model);
        }

        public ActionResult Edit(string id)
        {
            return View(CurrencyHelper.GetCurrency(id));
        }

        [HttpPost]
        public ActionResult Edit(CurrencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                string result = CurrencyHelper.SaveCurrency(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            CurrencyHelper.DeleteCurrency(id);
            return RedirectToAction("Index");
        }

        public ActionResult GetCurrencyList(long sobId)
        {
            CurrencyListModel model = new CurrencyListModel();
            model.SOBId = sobId;
            model.Currencies = CurrencyHelper.GetCurrencyList(sobId);
            return PartialView("_List", model);
        }

        public ActionResult CurrencyListPartial(long sobId)
        {
            return PartialView("_List", CurrencyHelper.GetCurrencyList(sobId));
        }
    }
}