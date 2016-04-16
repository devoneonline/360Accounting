﻿using _360Accounting.Web;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web.Controllers
{
    [Authorize]
    public class AccountController : AsyncController
    {
        public ActionResult AccountListPartial()
        {
            List<AccountViewModel> accountsList = AccountHelper.GetAccounts("", false, null, "", "");
            return PartialView("_List", accountsList);
        }

        public ActionResult Index(AccountListModel model)
        {
            model.Accounts = AccountHelper.GetAccounts(model.SearchText, true, model.Page, model.SortColumn, model.SortDirection);
            return View(model);
        }

        public ActionResult Create()
        {
            AccountCreateViewModel model = new AccountCreateViewModel();
            model.SOBId = SessionHelper.SOBId;
            return View("Edit", model);
        }

        public ActionResult Edit(string id)
        {
            AccountCreateViewModel model = AccountHelper.GetAccount(id);
            model.SOBId = SessionHelper.SOBId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AccountCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.SOBId = SessionHelper.SOBId;
                if (model.Id > 0)
                {
                    string result = AccountHelper.SaveChartOfAccount(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    if (AccountHelper.GetAccountBySOBId(model.SOBId.ToString()) == null)
                    {
                        string result = AccountHelper.SaveChartOfAccount(model);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Account Already exists.");
                    }
                }
            }

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            AccountHelper.Delete(id);
            return RedirectToAction("Index");
        }
    }
}