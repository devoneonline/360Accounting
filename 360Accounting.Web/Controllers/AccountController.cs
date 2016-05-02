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
    public class AccountController : BaseController //Inheritance change from AsyncController to BaseController
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
            if (AccountHelper.GetAccountBySOBId(SessionHelper.SOBId.ToString()) != null)
            {
                ViewBag.ErrorMessage = "One book can have maximum one chart of account!";
                return RedirectToAction("Index");
            }
            AccountCreateViewModel model = new AccountCreateViewModel();
            model.SOBId = SessionHelper.SOBId;
            return View("Edit", model);
        }

        public ActionResult Edit(string id)
        {
            AccountCreateViewModel model = AccountHelper.GetAccount(id);
            
            List<AccountValueViewModel> accountValues = AccountValueHelper.GetAccountValuesbyChartId(Convert.ToInt64(id), SessionHelper.SOBId);
            if (accountValues.Any())
                throw new Exception("Edit Error", new Exception { Source = "Accounts having values cannot be edited" });

            List<Core.Entities.CodeCombinition> codeCombinitions = CodeCombinationHelper.GetCodeCombinations(model.SOBId, AuthenticationHelper.CompanyId.Value);
            if (codeCombinitions.Any())
                throw new Exception("Edit Error", new Exception { Source = "Accounts having code combinitions cannot be edited" });

            model.SOBId = SessionHelper.SOBId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AccountCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.SegmentEnabled1 == false)
                {
                    ModelState.AddModelError("Error", "First segment should be enabled.");
                    return View(model);
                }
                model.SOBId = SessionHelper.SOBId;
                if (model.Id > 0)
                {
                    string result = AccountHelper.SaveChartOfAccount(model);
                    if (result.Contains("can not be marked as disabled"))
                    {
                        ModelState.AddModelError("Error", result);
                    }
                    else
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
            List<AccountValueViewModel> accountValues = AccountValueHelper.GetAccountValuesbyChartId(Convert.ToInt64(id), SessionHelper.SOBId);
            if (accountValues.Any())
                throw new Exception("Delete Error", new Exception { Source = "Accounts having values cannot be deleted" });

            List<Core.Entities.CodeCombinition> codeCombinitions = CodeCombinationHelper.GetCodeCombinations(SessionHelper.SOBId, AuthenticationHelper.CompanyId.Value);
            if (codeCombinitions.Any())
                throw new Exception("Delete Error", new Exception { Source = "Accounts having code combinitions cannot be edited" });

            AccountHelper.Delete(id);
            return RedirectToAction("Index");
        }
    }
}