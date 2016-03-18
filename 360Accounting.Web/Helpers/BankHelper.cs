using _360Accounting.Core;
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

        public static List<SelectListItem> GetBanks(long sobId)
        {
            throw new NotImplementedException();
        }

        internal static List<SelectListItem> GetBankAccounts(long sobId, long bankId)
        {
            throw new NotImplementedException();
        }
    }
}