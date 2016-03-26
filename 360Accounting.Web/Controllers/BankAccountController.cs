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
        private IBankService bankService;
        private IBankAccountService service;
        private ICodeCombinitionService codeCombinitionService;

        #region Private Methods
        private BankAccount mapModel(BankAccountViewModel model)
        {
            return new BankAccount
            {
                AccountName = model.AccountName,
                AdditionalInformation = model.AdditionalInformation,
                BankId = model.BankId,
                Cash_CCID = model.Cash_CCID,
                Confirm_CCID = model.Confirm_CCID,
                EndDate = model.EndDate,
                Id = model.Id,
                RemitCash_CCID = model.RemitCash_CCID,
                StartDate = model.StartDate
            };
        }
        #endregion

        public BankAccountController()
        {
            service = IoC.Resolve<IBankAccountService>("BankAccountService");
            bankService = IoC.Resolve<IBankService>("BankService");
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
                if (model.Id > 0)
                    result = service.Update(mapModel(model));
                else
                    result = service.Insert(mapModel(model));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            BankAccountViewModel model = new BankAccountViewModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
            if (model.CodeCombinition == null)
            {
                BankModel bank = BankHelper.GetBank(model.BankId.ToString());
                model.CodeCombinition = CodeCombinationHelper.GetAccounts(bank.SOBId, bank.StartDate, bank.EndDate).ToList();
            }
            return View("Create", model);
        }

        public ActionResult Delete(string id, long bankId)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
            return RedirectToAction("Index", new { Id = bankId });
        }

        public ActionResult BankAccountListPartial(long bankId)
        {
            IEnumerable<BankAccountViewModel> list = service
                .GetBankAccounts(bankId, AuthenticationHelper.User.CompanyId)
                .Select(a => new BankAccountViewModel(a)).ToList();
            return PartialView("_List", list);
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
                    if (model.Id > 0)
                        result = service.Update(mapModel(model));
                    else
                        result = service.Insert(mapModel(model));
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