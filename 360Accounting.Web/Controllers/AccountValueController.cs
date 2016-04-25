using _360Accounting.Common;
using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Data.Repositories;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class AccountValueController : Controller
    {
        public ActionResult Index(AccountValueListModel model, string message="")
        {
            ViewBag.ErrorMessage = message;
            model.SOBId = SessionHelper.SOBId;
            model.Segments = AccountHelper.GetSegmentList(model.SOBId.ToString());
            model.Segment = model.Segments[0].Value;
        
            return View(model);
        }

        public ActionResult Create(string segment)
        {
            AccountValueViewModel model = new AccountValueViewModel();
            Account account = AccountHelper.GetAccountBySOBId(SessionHelper.SOBId.ToString());
            if (account != null)
            {
                model.ChartId = account.Id;
                model.SetOfBook = SetOfBookHelper.GetSetOfBook(SessionHelper.SOBId.ToString()).Name;
                model.Segment = segment;
                //model.StartDate = Const.CurrentDate;
                //model.EndDate = Const.EndDate;
                model.ValueChar = AccountHelper.GetSegmentCharacters(segment, account);
                return View("Edit", model);
            }

            return RedirectToAction("Index");
        }

        public JsonResult SegmentList()
        {
            return Json(AccountHelper.GetSegmentList(SessionHelper.SOBId.ToString()), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Edit(string id)
        {
            AccountValueViewModel model = AccountValueHelper.GetAccountValue(id);
            model.SetOfBook = SetOfBookHelper.GetSetOfBook(SessionHelper.SOBId.ToString()).Name;
            model.ValueChar = AccountHelper.GetSegmentCharacters(model.Segment, AccountHelper.GetAccountBySOBId(SessionHelper.SOBId.ToString()));

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AccountValueViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Value.Length != model.ValueChar)
                        ModelState.AddModelError("Error", "Invalid Value character length.");
                    else if (model.StartDate != null && model.StartDate > model.EndDate)
                        ModelState.AddModelError("Error", "End Date should be greater than StartDate.");

                    ////else if (model.Id == 0 && model.EndDate != null && model.EndDate < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
                    ////{
                    ////    ModelState.AddModelError("Error", "End Date cannot be a back date.");
                    ////}
                    else
                    {
                        string result = AccountValueHelper.SaveChartOfAccountValue(model);
                        return RedirectToAction("Index", new { id = SessionHelper.SOBId });
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.InnerException.InnerException.Message);
                }
            }

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            try
            {
                AccountValueHelper.Delete(id);
                return RedirectToAction("Index", new { id = SessionHelper.SOBId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { id = SessionHelper.SOBId, message = ex.Message });
            }
        }

        public ActionResult AccountValuesPartial(string segment)
        {
            List<AccountValueViewModel> accountValuesList = AccountValueHelper.GetAccountValues(AccountHelper.GetAccountBySOBId(SessionHelper.SOBId.ToString()).Id, SessionHelper.SOBId, segment);
            return PartialView("_List", accountValuesList);
        }
    }
}