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
        public ActionResult Index(long id, AccountValueListModel model)
        {
            Session["sobid"] = id;   //TODO:: temporary
            model.SOBId = id;
            model.Segments = AccountHelper.GetSegmentList(model.SOBId.ToString());
            model.Segment = model.Segments[0].Value;
        
            return View(model);
        }

        public ActionResult Create(string sobId, string segment)
        {
            AccountValueViewModel model = new AccountValueViewModel();
            Account account = AccountHelper.GetAccountBySOBId(sobId);
            if (account != null)
            {
                model.ChartId = account.Id;
                model.SetOfBook = SetOfBookHelper.GetSetOfBook(sobId.ToString()).Name;
                model.Segment = segment;
                model.StartDate = Const.CurrentDate;
                model.EndDate = Const.EndDate;
                model.ValueChar = AccountHelper.GetSegmentCharacters(segment, account);
                SessionHelper.SOBId = Convert.ToInt32(sobId);   //TODO:: temporary
                return View("Edit", model);
            }

            return RedirectToAction("Index");
        }

        public JsonResult SegmentList(string sobId)
        {
            return Json(AccountHelper.GetSegmentList(sobId), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Edit(string id)
        {
            AccountValueViewModel model = AccountValueHelper.GetAccountValue(id);
            model.SetOfBook = SetOfBookHelper.GetSetOfBook
                (AccountHelper.GetAccount
                (model.ChartId.ToString()).SOBId.ToString()).Name;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AccountValueViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string result = AccountValueHelper.SaveChartOfAccountValue(model);
                    return RedirectToAction("Index", new { id = Session["sobid"] });
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
            AccountValueHelper.Delete(id);
            return RedirectToAction("Index", new { id = Session["sobid"] });
        }

        public ActionResult AccountValuesPartial(string sobId, string segment)
        {
            //List<AccountValueViewModel> accountValuesList = AccountValueHelper.GetAccountValues(AccountHelper.GetAccountBySOBId(sobId).Id, segment);
            List<AccountValueViewModel> accountValuesList = new List<AccountValueViewModel>();
            return PartialView("_List", accountValuesList);
        }
    }
}