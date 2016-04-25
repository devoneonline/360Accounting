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
    public class BankAccountController : Controller
    {
        public ActionResult Index(long Id, string message = "")
        {
            ViewBag.ErrorMessage = message;
            BankAccountListModel model = new BankAccountListModel();
            model.BankId = Id;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BankAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                string result = "";
                BankModel bank = BankHelper.GetBank(model.BankId.ToString());
                if ((model.StartDate >= bank.StartDate && model.EndDate <= bank.EndDate) ||
                    (model.StartDate == null && bank.StartDate == null ||
                    model.EndDate == null && bank.EndDate == null))
                {
                    result = BankHelper.SaveBankAccount(model);
                    return RedirectToAction("Index", new { Id = model.BankId });
                }
                else
                {
                    ModelState.AddModelError("Error", "Bank Account Dates should be within the range of Bank Dates.");
                }
            }
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            BankAccountViewModel model = BankHelper.GetBankAccount(id);

            CodeCombinitionCreateViewModel cashCode = CodeCombinationHelper.GetCodeCombination(model.Cash_CCID.ToString());
            CodeCombinitionCreateViewModel remitCode = CodeCombinationHelper.GetCodeCombination(model.RemitCash_CCID.ToString());
            CodeCombinitionCreateViewModel confirmCode = CodeCombinationHelper.GetCodeCombination(model.Confirm_CCID.ToString());
            
            model.Cash_CCIDString = Utility.Stringize(".", cashCode.Segment1, cashCode.Segment2, cashCode.Segment3, cashCode.Segment4, cashCode.Segment5, cashCode.Segment6, cashCode.Segment7, cashCode.Segment8);
            model.RemitCash_CCIDString = Utility.Stringize(".", remitCode.Segment1, remitCode.Segment2, remitCode.Segment3, remitCode.Segment4, remitCode.Segment5, remitCode.Segment6, remitCode.Segment7, remitCode.Segment8);
            model.Confirm_CCIDString = Utility.Stringize(".", confirmCode.Segment1, confirmCode.Segment2, confirmCode.Segment3, confirmCode.Segment4, confirmCode.Segment5, confirmCode.Segment6, confirmCode.Segment7, confirmCode.Segment8);
            
            //if (model.CodeCombinition == null)
            //{
            //    BankModel bank = BankHelper.GetBank(model.BankId.ToString());
            //    model.CodeCombinition = CodeCombinationHelper.GetAccounts(SessionHelper.SOBId, bank.StartDate, bank.EndDate).ToList();
            //}
            return View("Create", model);
        }

        public ActionResult Delete(string id, long bankId)
        {
            try
            {
                BankHelper.DeleteBankAccount(id);
                return RedirectToAction("Index", new { Id = bankId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { Id = bankId, message = ex.Message });
            }
        }

        public ActionResult BankAccountListPartial(string bankId)
        {
            return PartialView("_List", BankHelper.GetBankAccounts(bankId));
        }

        [HttpPost]
        public ActionResult Create(BankAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                string result = "";
                BankModel bank = BankHelper.GetBank(model.BankId.ToString());
                if ((model.StartDate >= bank.StartDate && model.EndDate <= bank.EndDate) ||
                    (model.StartDate == null && bank.StartDate == null ||
                    model.EndDate == null && bank.EndDate == null))
                {
                    result = BankHelper.SaveBankAccount(model);
                    return RedirectToAction("Index", new { Id = model.BankId });
                }
                else
                {
                    ModelState.AddModelError("Error", "Bank Account Dates should be within the range of Bank Dates.");
                }
            }
            return View(model);
        }

        public ActionResult Create(long bankId)
        {
            BankAccountViewModel bankAccount = new BankAccountViewModel();
            if (bankAccount.CodeCombinition == null)
            {
                BankModel bank = BankHelper.GetBank(bankId.ToString());
                bankAccount.CodeCombinition = CodeCombinationHelper.GetAccounts(SessionHelper.SOBId, bank.StartDate, bank.EndDate).ToList();
            }
            bankAccount.BankId = bankId;
            return View(bankAccount);
        }
    }
}