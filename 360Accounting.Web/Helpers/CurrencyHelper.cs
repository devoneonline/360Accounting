using _360Accounting.Core;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public static class CurrencyHelper
    {
        private static ICurrencyService service;

        static CurrencyHelper()
        {
            service = IoC.Resolve<ICurrencyService>("CurrencyService");
        }

        public static string SaveCurrency(CurrencyViewModel model)
        {
            if (model.Id > 0)
            {
                return service.Update(Mappers.GetEntityByModel(model));
            }
            else
            {
                return service.Insert(Mappers.GetEntityByModel(model));
            }
        }

        public static CurrencyViewModel GetCurrency(string id)
        {
            return new CurrencyViewModel(service.GetSingle(id, AuthenticationHelper.User.CompanyId));
        }

        public static void DeleteCurrency(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }

        public static List<CurrencyViewModel> GetCurrencyList(long sobId)
        {
            return service.GetAll(AuthenticationHelper.User.CompanyId, sobId)
                .Select(x => new CurrencyViewModel(x)).ToList();
        }
    }
}