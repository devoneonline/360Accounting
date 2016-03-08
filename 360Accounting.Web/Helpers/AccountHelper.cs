using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Service;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public static class AccountHelper
    {
        private static IAccountService service;

        static AccountHelper()
        {
            service = IoC.Resolve<IAccountService>("AccountService");
        }

        public static List<AccountViewModel> ListAccounts(string searchText, bool paging, int? page, string sort, string sortDir)
        {
            List<AccountViewModel> modelList = service
                .GetAll(AuthenticationHelper.User.CompanyId, searchText, paging, page, sort, sortDir)
                .Select(x => new AccountViewModel(x)).ToList();
            return modelList;
        }

        public static AccountCreateViewModel GetAccount(string id)
        {
            AccountCreateViewModel account = new AccountCreateViewModel
                (service.GetSingle(id, AuthenticationHelper.User.CompanyId));
            return account;
        }

        public static string Insert(AccountCreateViewModel model)
        {
            return service.Insert(Mappers.GetEntityByModel(model));
        }

        public static string Update(AccountCreateViewModel model)
        {
            return service.Update(Mappers.GetEntityByModel(model));
        }

        public static Account GetAccountBySOBId(long sobId)
        {
            return service.GetAccountBySOBId
                (sobId.ToString(), AuthenticationHelper.User.CompanyId);
        }

        internal static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }
    }
}