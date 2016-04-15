using _360Accounting.Common;
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
    public class BankController : Controller
    {
        public ActionResult Index()
        {
            BankListModel model = new BankListModel();
            model.SOBId = SessionHelper.SOBId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BankModel model)
        {
            if (ModelState.IsValid)
            {
                string result = BankHelper.SaveBank(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            return View("Create", BankHelper.GetBank(id));
        }

        public ActionResult Delete(string id)
        {
            BankHelper.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult BankListPartial()
        {
            IEnumerable<BankModel> list = BankHelper.GetBanks(SessionHelper.SOBId);
            return PartialView("_List", list);
        }

        [HttpPost]
        public ActionResult Create(BankModel model)
        {
            if (ModelState.IsValid)
            {
                string result = "";
                result = BankHelper.SaveBank(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Create()
        {
            BankModel bank = new BankModel();
            bank.SOBId = SessionHelper.SOBId;
            return View(bank);
        }

        public JsonResult CheckStartDate(DateTime startDate, long bankId)
        {
            bool returnData = true;
            if (bankId > 0)
            {
                BankModel bank = BankHelper.GetBank(bankId.ToString());
                if (startDate >= bank.StartDate)
                    returnData = true;
                else
                    returnData = false;
            }
            return Json(returnData);
        }

        public JsonResult CheckEndDate(DateTime endDate, long bankId)
        {
            bool returnData = true;
            if (bankId > 0)
            {
                BankModel bank = BankHelper.GetBank(bankId.ToString());
                if (endDate <= bank.EndDate)
                    returnData = true;
                else
                    returnData = false;
            }
            return Json(returnData);
        }
    }
}