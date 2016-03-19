using _360Accounting.Core;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _360Accounting.Web
{
    public static class BankHelper
    {
        private static IBankService service;
        private static IBankAccountService bankAccountservice;

        static BankHelper()
        {
            service = IoC.Resolve<IBankService>("BankService");
            bankAccountservice = IoC.Resolve<IBankAccountService>("BankAccountService");
        }

        public static List<BankModel> GetBanks(long sobId)
        {
            return service.GetBySOBId(sobId)
                .Select(x => new BankModel(x)).ToList();
        }

        public static List<BankAccountModel> GetBankAccounts(long bankId)
        {
            return bankAccountservice.GetBankAccounts(bankId, AuthenticationHelper.User.CompanyId)
                .Select(x => new BankAccountModel(x)).ToList();
        }

        public static List<SelectListItem> GetBankList(long sobId)
        {
            List<SelectListItem> bankList = GetBanks(sobId)
                .Select(x => new SelectListItem
                {
                    Text = x.BankName,
                    Value = x.Id.ToString()
                }).ToList();
            return bankList;

        }

        public static List<SelectListItem> GetBankAccountList(long bankId)
        {
            List<SelectListItem> bankAccountList = GetBankAccounts(bankId)
                .Select(x => new SelectListItem
                {
                    Text = x.AccountName,
                    Value = x.Id.ToString()
                }).ToList();
            return bankAccountList;
        }

        public static BankAccountModel GetBankAccount(string bankAccountId)
        {
            BankAccountModel bankAccount = new BankAccountModel(bankAccountservice
                .GetSingle(bankAccountId, AuthenticationHelper.User.CompanyId));
            return bankAccount;
        }

        internal static BankModel GetBank(string bankId)
        {
            BankModel bank = new BankModel(service
                .GetSingle(bankId, AuthenticationHelper.User.CompanyId));
            return bank;
        }
    }
}