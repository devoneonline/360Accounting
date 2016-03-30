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
        private ICodeCombinitionService codeCombinitionService;

        public BankAccountController()
        {
            codeCombinitionService = IoC.Resolve<ICodeCombinitionService>("CodeCombinitionService");
        }

        public ActionResult Index(long Id)
        {
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
                result = BankHelper.SaveBankAccount(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            BankAccountViewModel model = BankHelper.GetBankAccount(id);
            if (model.CodeCombinition == null)
            {
                BankModel bank = BankHelper.GetBank(model.BankId.ToString());
                model.CodeCombinition = CodeCombinationHelper.GetAccounts(bank.SOBId, bank.StartDate, bank.EndDate).ToList();
            }
            return View("Create", model);
        }

        public ActionResult Delete(string id, long bankId)
        {
            BankHelper.DeleteBankAccount(id);
            return RedirectToAction("Index", new { Id = bankId });
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
                bool validated = false;
                BankModel bank = BankHelper.GetBank(model.BankId.ToString());
                if (model.StartDate >= bank.StartDate && model.EndDate <= bank.EndDate)
                    validated = true;

                if (validated)
                {
                    result = BankHelper.SaveBankAccount(model);
                }

                return RedirectToAction("Index", new { Id = model.BankId });
            }
            return View(model);
        }

        public ActionResult Create(long bankId)
        {
            BankAccountViewModel bankAccount = new BankAccountViewModel();
            if (bankAccount.CodeCombinition == null)
            {
                BankModel bank = BankHelper.GetBank(bankId.ToString());
                bankAccount.CodeCombinition = CodeCombinationHelper.GetAccounts(bank.SOBId, bank.StartDate, bank.EndDate).ToList();
            }
            bankAccount.BankId = bankId;
            return View(bankAccount);
        }
    }
}