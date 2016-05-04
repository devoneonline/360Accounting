using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class UserSetofBookController : AsyncController
    {
        public ActionResult Index()
        {
            if (TempData["LastURL"] == null)
                TempData["LastURL"] = Request.UrlReferrer.AbsolutePath;

            UserSetofBookModel model = new UserSetofBookModel();
            //if(SessionHelper.SOBId != null && SessionHelper.SOBId != 0)
            if (SessionHelper.SOBId != 0)
            {
                model = UserSetofBookHelper.GetDefaultSOB();
                if(model != null)
                {
                    model.SetofBooks = SetOfBookHelper.GetSetOfBookList();
                    return PartialView("_Create", model);
                }
            }
            model = new UserSetofBookModel
            {
                SetofBooks = SetOfBookHelper.GetSetOfBookList(),
                CompanyId = AuthenticationHelper.CompanyId.Value,
                SOBId = SessionHelper.SOBId,
                UserId = AuthenticationHelper.UserId
            };

            return PartialView("_Create", model);
        }

        public JsonResult CheckUserSOB()
        {
            if (SessionHelper.SOBId == 0)
            {
                return Json(false);
            }
            return Json(true);
        }

        public JsonResult Save(UserSetofBookModel model)
        {
            UserSetofBookModel defaultSOB = UserSetofBookHelper.GetDefaultSOB();
            if (defaultSOB != null)
            {
                model.Id = defaultSOB.Id;
            }

            //Remove all transaction's session that depends on SOB..
            SessionHelper.JV = null;
            SessionHelper.Invoice = null;
            SessionHelper.PayableInvoice = null;
            SessionHelper.Payment = null;
            SessionHelper.Order = null;
            SessionHelper.MiscellaneousTransaction = null;
            SessionHelper.Locator = null;
            SessionHelper.Receipts = null;
            SessionHelper.Bank = null;
            SessionHelper.BankAccount = null;
            SessionHelper.Calendar = null;
            SessionHelper.Item = null;
            SessionHelper.Remittance = null;
            SessionHelper.Tax = null;

            model.UserId = AuthenticationHelper.UserId;
            model.CompanyId = AuthenticationHelper.CompanyId.Value;

            SessionHelper.SOBId = model.SOBId;
            SessionHelper.SOBName = SetOfBookHelper.GetSetOfBook(SessionHelper.SOBId.ToString()).Name;

            return Json(UserSetofBookHelper.Save(model));
        }

        public ActionResult SelectSOB()
        {
            return View("_SelectSOB");
        }

        public ActionResult ViewErrorPopup()
        {
            TempData["LastURL"] = TempData["LastURL"];
            return PartialView("_Exception");
        }
    }
}