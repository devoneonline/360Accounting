using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class InvoiceSourceController : Controller
    {
        public ActionResult Edit(string id)
        {
            InvoiceSourceViewModel model = InvoiceSourceHelper.GetInvoiceSource(id);

            CodeCombinitionViewModel codeCombination = CodeCombinationHelper.GetSingle(model.CodeCombinationId);
            model.CodeCombinations = new List<SelectListItem>();
            model.CodeCombinations.Add(new SelectListItem
                {
                    Text = codeCombination.CodeCombinitionCode,
                    Value = codeCombination.Id.ToString()
                });

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            InvoiceSourceHelper.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult InvoiceSourceListPartial()
        {
            List<InvoiceSourceViewModel> invoiceSourceList =
                InvoiceSourceHelper.InvoiceList(SessionHelper.SOBId);
            return PartialView("_List", invoiceSourceList);
        }

        [HttpPost]
        public ActionResult Edit(InvoiceSourceViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string result = InvoiceSourceHelper.SaveInvoiceSource(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }
                
            }
            return View(model);
        }

        public ActionResult Create()
        {
            InvoiceSourceViewModel model = new InvoiceSourceViewModel();
            model.SOBId = SessionHelper.SOBId;
            model.CodeCombinations = CodeCombinationHelper.GetAccounts(SessionHelper.SOBId, model.StartDate, model.EndDate);
            return View("Edit", model);
        }

        public ActionResult Index()
        {
            InvoiceSourceListModel model = new InvoiceSourceListModel();
            model.SOBId = SessionHelper.SOBId;
            return View(model);
        }
    }
}