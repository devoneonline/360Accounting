//using _360Accounting.Core;
//using _360Accounting.Core.Entities;
//using _360Accounting.Data.Repositories;
//using _360Accounting.Service;
using _360Accounting.Web;
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
            List<AccountViewModel> accountsList = AccountHelper.ListAccounts("", false, null, "", "");
            return PartialView("_List", accountsList);
        }

        public ActionResult Index(AccountListModel model)
        {
            model.Accounts = AccountHelper.ListAccounts(model.SearchText, true, model.Page, model.SortColumn, model.SortDirection);
            return View(model);
        }

        public ActionResult Create()
        {
            AccountCreateViewModel model = new AccountCreateViewModel();
            model.SetOfBooks = SetOfBookHelper.GetSetOfBook();
            return View("Edit", model);
        }

        public ActionResult Edit(string id)
        {
            AccountCreateViewModel model = AccountHelper.GetAccount(id);
            
            //SetOfBook sob = sobService.GetSingle(model.SOBId.ToString(), AuthenticationHelper.User.CompanyId);
            //model.SetOfBooks = new List<SelectListItem>();
            //model.SetOfBooks.Add(new SelectListItem { Text = sob.Name, Value = sob.Id.ToString() });

            model.SetOfBooks = SetOfBookHelper.GetSetOfBook();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AccountCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    string result = AccountHelper.Update(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    if (AccountHelper.GetAccountBySOBId(model.SOBId) == null)
                    {
                        string result = AccountHelper.Insert(model);    ////TODO: mapper should be in service
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Account Already exists.");
                        model.SetOfBooks = SetOfBookHelper.GetSetOfBook();
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