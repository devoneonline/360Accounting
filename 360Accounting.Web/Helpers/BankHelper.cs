using _360Accounting.Core;
using _360Accounting.Core.Entities;
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
        private static IBankAccountService bankAccountService;

        static BankHelper()
        {
            service = IoC.Resolve<IBankService>("BankService");
            bankAccountService = IoC.Resolve<IBankAccountService>("BankAccountService");
        }

        #region Private Methods
        private static BankAccount getEntityByModel(BankAccountViewModel model)
        {
            if (model == null) return null;

            BankAccount entity = new BankAccount();

            if (model.Id == 0)
            {
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CompanyId = model.CompanyId;
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }

            entity.AccountName = model.AccountName;
            entity.AdditionalInformation = model.AdditionalInformation;
            entity.BankId = model.BankId;
            entity.Cash_CCID = model.Cash_CCID;
            entity.Confirm_CCID = model.Confirm_CCID;
            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.RemitCash_CCID = model.RemitCash_CCID;
            entity.StartDate = model.StartDate;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;

            return entity;
        }

        private static Bank getEntityByModel(BankModel model)
        {
            if (model == null) return null;

            Bank entity = new Bank();

            if (model.Id == 0)
            {
                entity.CompanyId = AuthenticationHelper.CompanyId.Value;
                entity.CreateBy = AuthenticationHelper.UserId;
                entity.CreateDate = DateTime.Now;
            }
            else
            {
                entity.CompanyId = model.CompanyId;
                entity.CreateBy = model.CreateBy;
                entity.CreateDate = model.CreateDate;
            }

            entity.BankName = model.BankName;
            entity.EndDate = model.EndDate;
            entity.Id = model.Id;
            entity.Remarks = model.Remarks;
            entity.SOBId = model.SOBId;
            entity.StartDate = model.StartDate;
            entity.UpdateBy = AuthenticationHelper.UserId;
            entity.UpdateDate = DateTime.Now;

            return entity;
        }
        #endregion

        public static List<BankModel> GetBanks(long sobId)
        {
            return service.GetBySOBId(sobId)
                .Select(x => new BankModel(x)).ToList();
        }

        public static List<BankAccountModel> GetBankAccounts(long bankId)
        {
            return bankAccountService.GetBankAccounts(bankId, AuthenticationHelper.CompanyId.Value)
                .Select(x => new BankAccountModel(x)).ToList();
        }

        public static List<BankAccountViewModel> GetBankAccounts(string bankId)
        {
            return bankAccountService.GetBankAccounts(Convert.ToInt64(bankId), AuthenticationHelper.CompanyId.Value)
                .Select(x => new BankAccountViewModel(x)).ToList();
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

        public static BankAccountModel GetBankAccount(long bankAccountId)
        {
            BankAccountModel bankAccount = new BankAccountModel(bankAccountService
                .GetSingle(bankAccountId.ToString(), AuthenticationHelper.CompanyId.Value));
            return bankAccount;
        }

        public static BankAccountViewModel GetBankAccount(string bankAccountId)
        {
            BankAccountViewModel bankAccount = new BankAccountViewModel(bankAccountService.GetSingle(bankAccountId, AuthenticationHelper.CompanyId.Value));
            return bankAccount;
        }

        public static BankModel GetBank(string bankId)
        {
            BankModel bank = new BankModel(service
                .GetSingle(bankId, AuthenticationHelper.CompanyId.Value));
            return bank;
        }

        public static string SaveBank(BankModel model)
        {
            if (model.Id > 0)
                return service.Update(getEntityByModel(model));
            else
                return service.Insert(getEntityByModel(model));
        }

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.CompanyId.Value);
        }

        public static string SaveBankAccount(BankAccountViewModel model)
        {
            if (model.Id > 0)
                return bankAccountService.Update(getEntityByModel(model));
            else
                return bankAccountService.Insert(getEntityByModel(model));
        }

        public static void DeleteBankAccount(string id)
        {
            bankAccountService.Delete(id, AuthenticationHelper.CompanyId.Value);
        }
    }
}