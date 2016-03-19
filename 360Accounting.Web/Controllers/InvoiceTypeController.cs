using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using _360Accounting.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class InvoiceTypeController : Controller
    {
        public ActionResult Index(InvoiceTypeListModel model)
        {
            if (model.SetOfBooks == null)
            {
                model.SetOfBooks = SetOfBookHelper.GetSetOfBooks()
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
            }
            model.SOBId = model.SOBId > 0 ? model.SOBId : model.SetOfBooks != null ? model.SetOfBooks.Count() > 0 ? Convert.ToInt64(model.SetOfBooks[0].Value.ToString()) : 0 : 0;

            SessionHelper.SOBId = model.SOBId;
            
            return View(model);
        }

        public ActionResult CreatePartial()
        {
            return PartialView("_List", InvoiceTypeHelper.GetInvoiceTypes(SessionHelper.SOBId));
        }

        public ActionResult GetInvoiceTypes(long sobId)
        {
            SessionHelper.SOBId = sobId;
            return PartialView("_List", InvoiceTypeHelper.GetInvoiceTypes(SessionHelper.SOBId));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewInline(InvoiceTypeModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.SOBId = SessionHelper.SOBId;
                    InvoiceTypeHelper.Save(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", InvoiceTypeHelper.GetInvoiceTypes(SessionHelper.SOBId));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateInline(InvoiceTypeModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.SOBId = SessionHelper.SOBId;
                    InvoiceTypeHelper.Save(model);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", InvoiceTypeHelper.GetInvoiceTypes(SessionHelper.SOBId));
        }

        public ActionResult DeleteInline(InvoiceTypeModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    InvoiceTypeHelper.Delete(model.Id.ToString());
                    return PartialView("_List", InvoiceTypeHelper.GetInvoiceTypes(SessionHelper.SOBId));
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_List", InvoiceTypeHelper.GetInvoiceTypes(SessionHelper.SOBId));
        }
    }
}