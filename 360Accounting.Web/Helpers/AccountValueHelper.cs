using _360Accounting.Core;
using _360Accounting.Core.Entities;
using _360Accounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _360Accounting.Web
{
    public static class AccountValueHelper
    {
        private static IAccountValueService service;

        static AccountValueHelper()
        {
            service = IoC.Resolve<IAccountValueService>("AccountValueService");
        }

        public static List<AccountValueViewModel> GetAccountValues(long chartId, string segment)
        {
            return service.GetAccountValuesBySegment(chartId, segment).Select(x => new AccountValueViewModel(x)).ToList();
        }

        public static AccountValueViewModel GetAccountValue(string id)
        {
            return new AccountValueViewModel
                (service.GetSingle(id, AuthenticationHelper.User.CompanyId));
        }

        public static string SaveChartOfAccountValue(AccountValueViewModel model)
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

        public static void Delete(string id)
        {
            service.Delete(id, AuthenticationHelper.User.CompanyId);
        }

        
    }
}