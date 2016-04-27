using _360Accounting.Core;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    public class InvoiceSourceController : BaseController
    {
        public ActionResult Edit(string id)
        {
            InvoiceSourceViewModel model = InvoiceSourceHelper.GetInvoiceSource(id);

            CodeCombinitionCreateViewModel codeCombination = CodeCombinationHelper.GetCodeCombination(model.CodeCombinationId.ToString());
            model.CodeCombinationIdString = Utility.Stringize(".", codeCombination.Segment1, codeCombination.Segment2, codeCombination.Segment3,
                codeCombination.Segment4, codeCombination.Segment5, codeCombination.Segment6, codeCombination.Segment7, codeCombination.Segment8);

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            try
            {
                InvoiceSourceHelper.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { message = ex.Message });
            }
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

        public ActionResult Index(string message="")
        {
            ViewBag.ErrorMessage = message;
            InvoiceSourceListModel model = new InvoiceSourceListModel();
            model.SOBId = SessionHelper.SOBId;
            return View(model);
        }
    }
}